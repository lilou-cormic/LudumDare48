using UnityEngine;

public class MoveAndWrapUp : MoveAndWrap
{
    protected override Vector3 Direction => Vector3.up;

    protected override float Threshold => 7.5f;

    protected override float MoveDistance => 15;

    protected override float Speed => 1;

    protected override bool IsAtLimit()
    {
        return transform.position.y >= Threshold;
    }
}
