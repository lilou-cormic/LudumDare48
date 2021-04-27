using PurpleCable;
using UnityEngine;

public class ElectrifyOnTouch : MonoBehaviour
{
    private bool _hasElectrified = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasElectrified)
            return;

        _hasElectrified = true;

        collision.GetComponentInParent<Player>()?.Electrify();

        if (collision.GetComponentInParent<Player>()?.HasShieldDown() == true)
            collision.GetComponent<Health>()?.ChangeHP(-1);
    }
}