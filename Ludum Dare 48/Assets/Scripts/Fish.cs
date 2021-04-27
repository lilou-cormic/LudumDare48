using PurpleCable;
using UnityEngine;

public class Fish : PoolableMonoBehaviour
{
    [SerializeField] FishDef Def = null;

    private bool _isDead = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead)
            return;

        _isDead = true;

        Skeleton skeleton = SkeletonPool.Current.GetItem();
        skeleton.Set(Def);
        skeleton.transform.position = transform.position;
        skeleton.FadeOut();

        SetAsAvailable();

        if (collision.CompareTag("PlayerBullet"))
            ScoreManager.AddPoints(10);
    }

    protected override void Init()
    {
        _isDead = false;
    }
}
