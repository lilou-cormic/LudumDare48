using PurpleCable;
using UnityEngine;

public class ShieldPickup : PoolableItem
{
    protected override void OnPickup(Collider2D collision)
    {
        collision.GetComponentInParent<Player>()?.ActivateShield();
    }
}
