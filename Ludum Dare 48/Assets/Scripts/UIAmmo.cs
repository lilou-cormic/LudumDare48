using System.Collections;
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

        StartCoroutine(Pop());
    }

    private IEnumerator Pop()
    {
        for (int i = 0; i < AmmoPoints.Length; i++)
        {
            AmmoPoints[i].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < AmmoPoints.Length; i++)
        {
            AmmoPoints[i].transform.localScale = Vector3.one;
        }
    }

    private void Player_TorpedoCountChanged()
    {
        Set();
    }
}