using PurpleCable;

public class SkeletonPool : PrefabPool<Skeleton>
{
    public static SkeletonPool Current { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Current = this;
    }
}
