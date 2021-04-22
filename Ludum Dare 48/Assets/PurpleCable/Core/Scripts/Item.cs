using UnityEngine;

namespace PurpleCable
{
    /// <summary>
    /// Item that can be picked up [MonoBehaviour]
    /// </summary>
    public abstract class Item : MonoBehaviour
    {
        //TODO Couple with ItemDef

        #region Editor
        /// <summary>
        /// Pick up sound
        /// </summary>
        [SerializeField] AudioClip PickupSound = null;

        #endregion

        #region Data

        /// <summary>
        /// Indicates if the item has already been taken [To manage multi-frames / simultanious collisions]
        /// </summary>
        protected bool IsTaken { get; set; } = false;

        #endregion

        #region Unity callbacks

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Do nothing if already taken [To manage multi-frames / simultanious collisions]
            if (IsTaken)
                return;

            // Do nothing if it's determined that it can't be picked up
            if (!CanPickup(collision))
                return;

            // Flag as taken
            IsTaken = true;

            // Play pick up sound
            PickupSound.Play();

            // Do logic for when pick up (add to inventory, heal, etc.)
            OnPickup(collision);

            // Dispose of the item (destroy, add to pool, etc.)
            Dispose();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the item can be picked up (true by default) [override for logic (check stats, etc.)]
        /// </summary>
        /// <param name="collision">What collided with the item</param>
        /// <returns>If the item can be picked up</returns>
        protected virtual bool CanPickup(Collider2D collision)
        {
            return true;
        }

        /// <summary>
        /// Action to perform with item is picked up [override for logic (add to inventory, heal, etc.)]
        /// </summary>
        /// <param name="collision">What collided with the item</param>
        protected abstract void OnPickup(Collider2D collision);

        /// <summary>
        /// Action to perform to dispose of the object after it's been used
        /// </summary>
        protected virtual void Dispose()
        {
            // Destroy instance
            Destroy(gameObject);
        }

        #endregion
    }
}
