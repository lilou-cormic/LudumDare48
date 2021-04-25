using System;
using UnityEngine;

public abstract class MoveAndWrap : MonoBehaviour
{
    protected abstract Vector3 Direction { get; }

    protected abstract float Threshold { get; }

    protected abstract float MoveDistance { get; }

    protected abstract float Speed { get; }

    private void Start()
    {
        InvokeRepeating(nameof(Move), 0.02f, 0.02f);
    }

    private void Update()
    {
        if (IsAtLimit())
            transform.position -= Direction * MoveDistance;
    }

    private void Move()
    {
        if (!gameObject.activeSelf)
            return;

        transform.position += Direction * (float)Math.Round(0.02f * Speed * GameManager.Speed, 2);
    }

    protected virtual bool IsAtLimit()
    {
        return transform.position.y >= Threshold;
    }
}
