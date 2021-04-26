using PurpleCable;
using UnityEngine;

public class ExplodeOnTouch : MonoBehaviour
{
    private bool _hasExploded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasExploded)
            return;

        _hasExploded = true;

        ExplosionManager.SpawnExplosion(transform.position);

        collision.GetComponent<Health>()?.ChangeHP(-1);
    }
}
