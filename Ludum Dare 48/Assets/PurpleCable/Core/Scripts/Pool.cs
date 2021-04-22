using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public abstract class Pool<TPoolable> : MonoBehaviour, IEnumerable<TPoolable>
        where TPoolable : IPoolable
    {
        private List<TPoolable> _list;

        [SerializeField]
        protected int BatchCount = 10;

        protected virtual void Awake()
        {
            _list = new List<TPoolable>(BatchCount);
        }

        protected abstract TPoolable CreateItem();

        public TPoolable GetItem()
        {
            TPoolable item = _list.FirstOrDefault(x => !x.IsInUse);

            if (item == null)
            {
                for (int i = 0; i < BatchCount; i++)
                {
                    _list.Add(CreateItem());
                }

                item = _list.FirstOrDefault(x => !x.IsInUse);
            }

            item.SetAsInUse();

            return item;
        }

        IEnumerator<TPoolable> IEnumerable<TPoolable>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    public abstract class Pool<TPoolable, TCategory> : MonoBehaviour, IEnumerable<TPoolable>
        where TPoolable : IPoolable
    {
        private Dictionary<TCategory, List<TPoolable>> _lists;

        [SerializeField]
        protected int BatchCount = 10;

        protected virtual void Awake()
        {
            _lists = GetInitialLists();
        }

        protected abstract Dictionary<TCategory, List<TPoolable>> GetInitialLists();

        protected abstract TPoolable CreateItem(TCategory category);

        public TPoolable GetItem(TCategory category)
        {
            var list = _lists[category];

            TPoolable item = list.FirstOrDefault(x => !x.IsInUse);

            if (item == null)
            {
                for (int i = 0; i < BatchCount; i++)
                {
                    list.Add(CreateItem(category));
                }

                item = list.FirstOrDefault(x => !x.IsInUse);
            }

            item.SetAsInUse();

            return item;
        }

        IEnumerator<TPoolable> IEnumerable<TPoolable>.GetEnumerator()
        {
            return _lists.SelectMany(x => x.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _lists.SelectMany(x => x.Value).GetEnumerator();
        }
    }
}
