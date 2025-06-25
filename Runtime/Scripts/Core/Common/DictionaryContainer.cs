using JFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JFramework
{
    public class DictionaryContainer<T> : IDictionaryContainer<T>
    {
        public event Action<ICollection<T>> onItemAdded;
        public event Action<T> onItemRemoved;
        public event Action<T> onItemUpdated;

        private readonly Dictionary<string, T> _dictionary = new Dictionary<string, T>();
        private readonly Func<T, string> _keySelector;

        /// <summary>
        /// 构造函数，需要指定如何从T获取键
        /// </summary>
        /// <param name="keySelector">从T对象获取键的函数</param>
        public DictionaryContainer(Func<T, string> keySelector)
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
        }

        /// <summary>
        /// 添加单个项
        /// </summary>
        public void Add(T member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            var key = _keySelector(member);
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace");

            _dictionary.Add(key, member);
            onItemAdded?.Invoke(new List<T> { member });
        }

        /// <summary>
        /// 添加多个项
        /// </summary>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var addedItems = new List<T>();
            foreach (var item in collection)
            {
                if (item == null) continue;

                var key = _keySelector(item);
                if (string.IsNullOrWhiteSpace(key)) continue;

                _dictionary[key] = item;
                addedItems.Add(item);
            }

            if (addedItems.Count > 0)
            {
                onItemAdded?.Invoke(addedItems);
            }
        }

        /// <summary>
        /// 清空容器
        /// </summary>
        public void Clear()
        {
            if (_dictionary.Count == 0) return;

            var removedItems = _dictionary.Values.ToList();
            _dictionary.Clear();

            foreach (var item in removedItems)
            {
                onItemRemoved?.Invoke(item);
            }
        }

        /// <summary>
        /// 获取容器中项的数量
        /// </summary>
        public int Count() => _dictionary.Count;

        /// <summary>
        /// 根据键获取项
        /// </summary>
        public T Get(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                throw new ArgumentNullException(nameof(uid));

            return _dictionary.TryGetValue(uid, out var item)
                ? item
                : throw new KeyNotFoundException($"Item with key '{uid}' not found");
        }

        /// <summary>
        /// 根据条件筛选项
        /// </summary>
        public List<T> Get(Predicate<T> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return _dictionary.Values.Where(x => predicate(x)).ToList();
        }

        /// <summary>
        /// 获取所有项
        /// </summary>
        public List<T> GetAll() => _dictionary.Values.ToList();

        /// <summary>
        /// 移除指定项
        /// </summary>
        public bool Remove(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                throw new ArgumentNullException(nameof(uid));

            if (!_dictionary.TryGetValue(uid, out var item))
                return false;

            _dictionary.Remove(uid);
            onItemRemoved?.Invoke(item);
            return true;
        }

        /// <summary>
        /// 更新项
        /// </summary>
        public void Update(T member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            var key = _keySelector(member);
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace");

            if (!_dictionary.ContainsKey(key))
                throw new KeyNotFoundException($"Item with key '{key}' not found");

            _dictionary[key] = member;
            onItemUpdated?.Invoke(member);
        }

        /// <summary>
        /// 尝试获取项
        /// </summary>
        public bool TryGet(string uid, out T item)
        {
            if (string.IsNullOrWhiteSpace(uid))
            {
                item = default;
                return false;
            }

            return _dictionary.TryGetValue(uid, out item);
        }


    }
}
