using PurpleCable;
using UnityEngine;

public class HealthPickup : PoolableItem
{
    protected override void OnPickup(Collider2D collision)
    {
        collision.GetComponent<Health>().ChangeHP(1);
    }
}
