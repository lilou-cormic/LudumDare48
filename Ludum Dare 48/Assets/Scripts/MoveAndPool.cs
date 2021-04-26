using PurpleCable;

public class MoveAndPool : MoveAndWrapUp
{
    protected override void OnLimitReached()
    {
        GetComponent<IPoolable>()?.SetAsAvailable();
    }
}
