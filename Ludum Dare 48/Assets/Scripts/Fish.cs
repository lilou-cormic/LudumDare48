using PurpleCable;
using System;
using UnityEngine;

public class Fish : MonoBehaviour, IPoolable
{
    private void Start()
    {
        InvokeRepeating(nameof(MoveUp), 0.02f, 0.02f);
    }

    private void Update()
    {
        if (transform.position.y >= 7)
            SetAsAvailable();
    }

    private void MoveUp()
    {
        if (!gameObject.activeSelf)
            return;

        transform.position += Vector3.up * (float)Math.Round(0.02f * GameManager.Speed, 2);
    }

    #region IPoolable

    public bool IsInUse => gameObject.activeSelf;

    public void SetAsAvailable()
    {
        gameObject.SetActive(false);
    }

    public void SetAsInUse()
    {
        gameObject.SetActive(true);
    }

    #endregion
}
