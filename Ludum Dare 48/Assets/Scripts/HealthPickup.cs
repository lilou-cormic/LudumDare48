using PurpleCable;
using UnityEngine;

public class HealthPickup : PoolableItem
{
    protected override void OnPickup(Collider2D collision)
    {
        collision.GetComponentInParent<Health>().ChangeHP(1);
    }
}
