using System;

namespace JFramework
{
    public abstract class JObjectPool : IObjectPool
    {
        public JObjectPool(ITypeRegister typeRegister) {
        

        
        }
        public abstract T Rent<T>(Action<T> onGet = null);
        public abstract void Return<T>(T obj);
    }
}

