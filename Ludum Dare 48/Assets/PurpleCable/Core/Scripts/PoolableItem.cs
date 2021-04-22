namespace PurpleCable
{
    public abstract class PoolableItem : Item, IPoolable
    {
        protected override void Dispose()
        {
            ((IPoolable)this).SetAsAvailable();
        }

        bool IPoolable.IsInUse
            => gameObject.activeSelf;

        void IPoolable.SetAsInUse()
        {
            IsTaken = false;

            Init();

            gameObject.SetActive(true);
        }

        protected virtual void Init()
        { }

        void IPoolable.SetAsAvailable()
           => gameObject.SetActive(false);
    }
}
