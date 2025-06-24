using JFramework.Game;
using JFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFramework.Game
{
    public class JConfigManager
    {
        private readonly Dictionary<Type, object> _tables = new Dictionary<Type, object>();
        private readonly IConfigLoader loader;
        private readonly Dictionary<Type, Dictionary<string, IUnique>> _uidMaps = new Dictionary<Type, Dictionary<string, IUnique>>();
        private readonly Dictionary<Type, TableInfo> _registrations = new Dictionary<Type, TableInfo>();

        /// <summary>
        /// 反序列化
        /// </summary>
        //IDeserializer deserializer;

        public class TableInfo
        {
            public string Path;
            public Type TableType;
            public Type ItemType;
            public IDeserializer Deserializer;
        }

        public JConfigManager(IConfigLoader loader)
        {
            this.loader = loader;
            //this.deserializer = serializer;
        }


        /// <summary>
        /// 注册配置表
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="path"></param>
        public void RegisterTable<TTable, TItem>(string path, IDeserializer deserializer)
            where TTable : IConfigTable<TItem>, new()
            where TItem : IUnique
        {
            _registrations[typeof(TItem)] = new TableInfo
            {
                Path = path,
                TableType = typeof(TTable),
                ItemType = typeof(TItem),
                Deserializer = deserializer
                
            };

            // 预初始化UID映射
            _uidMaps[typeof(TItem)] = new Dictionary<string, IUnique>();
        }

        /// <summary>
        /// 预加载所有注册的配置表
        /// </summary>
        public async Task PreloadAllAsync()
        {
            var loadTasks = _registrations.Values.Select((tableInfo)=> LoadTableAsync(tableInfo, tableInfo.Deserializer));
            await Task.WhenAll(loadTasks);
        }

        /// <summary>
        /// 加载单个配置表
        /// </summary>
        private async Task LoadTableAsync(TableInfo tableInfo, IDeserializer deserializer)
        {
            // 1. 创建表实例（无需强制转换）
            var table = Activator.CreateInstance(tableInfo.TableType);

            // 2. 加载原始数据
            var data = await loader.LoadBytesAsync(tableInfo.Path);

             var itemList = deserializer.ToObject(data, tableInfo.ItemType.MakeArrayType());
            //to do: 序列化成

            // 3. 通过反射调用Initialize方法
            var initializeMethod = tableInfo.TableType.GetMethod("Initialize");
            initializeMethod.Invoke(table, new object[] { itemList });

            // 4. 存储表引用
            _tables[tableInfo.ItemType] = table;

            // 5. 构建UID索引
            var uidMap = _uidMaps[tableInfo.ItemType];
            uidMap.Clear();

            // 通过IEnumerable接口遍历（非泛型方式）
            var enumerable = (IEnumerable)table;
            foreach (var item in enumerable)
            {
                if (item is IUnique uniqueItem && !string.IsNullOrEmpty(uniqueItem.Uid))
                {
                    uidMap[uniqueItem.Uid] = uniqueItem;
                }
            }
        }

        /// <summary>
        /// 获取指定uid对象
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="uid"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public TItem Get<TItem>(string uid) where TItem : class, IUnique
        {
            if (_uidMaps.TryGetValue(typeof(TItem), out var map) &&
                map.TryGetValue(uid, out var item))
            {
                return (TItem)item;
            }
            throw new KeyNotFoundException($"{typeof(TItem)}[{uid}] not found");
        }


        /// <summary>
        /// 获取所有对象
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public List<TItem> GetAll<TItem>() where TItem : class, IUnique
        {
            if (_tables.TryGetValue(typeof(TItem), out var table))
            {
                return ((IConfigTable<TItem>)table).ToList();
            }
            return new List<TItem>();
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public List<TItem> Get<TItem>(Func<TItem, bool> predicate) where TItem : class, IUnique
        {
            // 1. 尝试获取已加载的表
            if (!_tables.TryGetValue(typeof(TItem), out var tableObj))
            {
                return new List<TItem>();
            }

            // 2. 类型安全转换
            if (!(tableObj is IConfigTable<TItem> table))
            {
                throw new InvalidCastException($"Table type mismatch for {typeof(TItem)}");
            }

            // 3. 应用筛选条件
            try
            {
                return predicate != null
                    ? table.Where(predicate).ToList()
                    : table.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Predicate evaluation failed", ex);
            }
        }
    }
}
