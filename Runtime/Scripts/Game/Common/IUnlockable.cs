using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Game
{
    /// <summary>
    /// 可以被解锁
    /// </summary>
    public interface IUnlockable
    {
        void Unlock();

        void Lock();
    }

    public interface IUnlockableContainer
    {
        void Unlock(string uid);

        void Lock(string uid);
    }
}
