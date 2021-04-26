using PurpleCable;

public class MoveAndPool2 : MoveAndWrapUp
{
    protected override float Speed => 1.5f + GameManager.Speed;

    protected override void OnLimitReached()
    {
        GetComponent<IPoolable>()?.SetAsAvailable();
    }
}
