using PurpleCable;
using UnityEngine;

public class TorpedoPickup : PoolableItem
{
    protected override void OnPickup(Collider2D collision)
    {
        collision.GetComponentInParent<Player>()?.AddTorpedo();
    }
}
