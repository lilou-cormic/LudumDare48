using UnityEngine;

namespace PurpleCable
{
    public abstract class PrefabPool<TPoolable> : Pool<TPoolable>
        where TPoolable : MonoBehaviour, IPoolable
    {
        [SerializeField] TPoolable Prefab = null;

        protected override TPoolable CreateItem()
        {
            var item = Instantiate(Prefab, transform);
            item.SetAsAvailable();

            return item;
        }
    }

    public abstract class PrefabPool<TPoolable, TCategory> : Pool<TPoolable, TCategory>
        where TPoolable : MonoBehaviour, IPoolable
    {
        protected abstract TPoolable GetPrefab(TCategory category);

        protected override TPoolable CreateItem(TCategory category)
        {
            var item = Instantiate(GetPrefab(category), transform);
            item.SetAsAvailable();

            return item;
        }
    }
}
