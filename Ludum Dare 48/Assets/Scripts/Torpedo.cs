using PurpleCable;
using UnityEngine;

public class Torpedo : PoolableMonoBehaviour
{    
    private void Update()
    {
        if (transform.position.y < -8)
            SetAsAvailable();

        transform.position += Vector3.down * Time.deltaTime * 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ExplosionManager.SpawnExplosion(transform.position);

        SetAsAvailable();
    }
}
