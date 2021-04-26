using PurpleCable;
using UnityEngine;

public class Fish : PoolableMonoBehaviour
{
    [SerializeField] FishDef Def = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Skeleton skeleton = SkeletonPool.Current.GetItem();
        skeleton.Set(Def);
        skeleton.transform.position = transform.position;
        skeleton.FadeOut();

        SetAsAvailable();

        if (collision.CompareTag("PlayerBullet"))
            ScoreManager.AddPoints(10);
    }
}
