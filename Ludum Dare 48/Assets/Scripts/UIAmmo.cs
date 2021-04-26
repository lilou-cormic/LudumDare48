using UnityEngine;

public class UIAmmo : MonoBehaviour
{
    [SerializeField] Player Player = null;

    [SerializeField] GameObject[] AmmoPoints = null;

    private void Awake()
    {
        Player.TorpedoCountChanged += Player_TorpedoCountChanged;
    }

    private void Start()
    {
        Set();
    }

    private void OnDestroy()
    {
        Player.TorpedoCountChanged -= Player_TorpedoCountChanged;
    }

    private void Set()
    {
        for (int i = 0; i < AmmoPoints.Length; i++)
        {
            AmmoPoints[i].gameObject.SetActive(i < Player.TorpedoCount);
        }
    }

    private void Player_TorpedoCountChanged()
    {
        Set();
    }
}