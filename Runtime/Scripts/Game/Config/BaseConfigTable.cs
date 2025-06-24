using System.Collections;
using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class BaseConfigTable<T> : IConfigTable<T>, IEnumerable<T> where T : IUnique
    {
        private readonly List<T> _items = new List<T>();

        public void Initialize(T[] lst) => _items.AddRange(lst);

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
