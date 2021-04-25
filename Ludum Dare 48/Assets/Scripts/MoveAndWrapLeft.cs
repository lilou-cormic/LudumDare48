using UnityEngine;

public class MoveAndWrapLeft : MoveAndWrap
{
    protected override Vector3 Direction => Vector3.left;

    protected override float Threshold => -4.5f;

    protected override float MoveDistance => 9;

    [SerializeField] float _Speed = 1f;
    protected override float Speed => _Speed;

    protected override bool IsAtLimit()
    {
        return transform.position.x <= Threshold;
    }
}
