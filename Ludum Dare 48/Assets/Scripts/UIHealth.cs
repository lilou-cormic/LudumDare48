using PurpleCable;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    [SerializeField] Health Health = null;

    private UIHealthPoint[] healthPoints;

    private void Awake()
    {
        healthPoints = GetComponentsInChildren<UIHealthPoint>();

        Health.HPChanged += Health_HPChanged;
    }

    private void Start()
    {
        Set();
    }

    private void OnDestroy()
    {
        Health.HPChanged -= Health_HPChanged;
    }

    private void Set()
    {
        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].gameObject.SetActive(i < Health.MaxHP);
            healthPoints[i].Set(i < Health.CurrentHP);
        }
    }

    private void Health_HPChanged(int amount)
    {
        Set();
    }
}
