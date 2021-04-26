using PurpleCable;
using UnityEngine;

public class TorpedoPickup : PoolableItem
{
    protected override void OnPickup(Collider2D collision)
    {
        collision.GetComponent<Player>().AddTorpedo();
    }
}
