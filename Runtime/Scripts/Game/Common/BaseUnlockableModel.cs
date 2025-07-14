using System;
using System.Xml.Linq;

namespace JFramework.Game
{

    /// <summary>
    /// 可以解锁节点的类型
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TUnlockableData"></typeparam>
    public abstract class BaseUnlockableModel<TData, TUnlockableData> : DictionaryContainer<TUnlockableData>, IUnlockableContainer where TUnlockableData : IUnlockable
    {
        protected EventManager eventManager;

        protected TData data;
        public TData Data => data;

        protected BaseUnlockableModel(Func<TUnlockableData, string> keySelector, EventManager eventManager) : base(keySelector)
        {
            if (eventManager == null)
                throw new Exception(this.GetType().ToString() + "inject eventManager failed, it is null !");

            this.eventManager = eventManager;
        }

        /// <summary>
        /// 加锁
        /// </summary>
        /// <param name="uid"></param>
        public virtual void Lock(string uid)
        {
            var data = Get(uid);
            data.Lock();
            Update(data);
        }

        /// <summary>
        /// 解锁指定对象
        /// </summary>
        /// <param name="uid"></param>
        public virtual void Unlock(string uid)
        {
            var data = Get(uid);
            data.Unlock();
            Update(data);
        }

        /// <summary>
        /// 初始化模型
        /// </summary>
        /// <param name="vo"></param>
        public virtual void Initialize(TData vo)
        {
            this.data = vo;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        protected void SendEvent<T>(object arg) where T : Event, new()
        {
            eventManager.Raise<T>(arg);
        }
    }
}