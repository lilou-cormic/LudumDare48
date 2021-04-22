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
}
