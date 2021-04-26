using UnityEngine;

namespace PurpleCable
{
    public abstract class PoolableMonoBehaviour : MonoBehaviour, IPoolable
    {
        bool IPoolable.IsInUse
            => gameObject.activeSelf;

        void IPoolable.SetAsInUse()
        {
            Init();

            gameObject.SetActive(true);
        }

        protected virtual void Init()
        { }

        protected void SetAsAvailable()
           => gameObject.SetActive(false);

        void IPoolable.SetAsAvailable() => SetAsAvailable();
    }
}
