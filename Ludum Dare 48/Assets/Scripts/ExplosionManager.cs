using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    private static ExplosionManager _instance;

    [SerializeField]
    public GameObject ExplosionPrefab = null;

    [SerializeField]
    public AudioClip ExplosionSound = null;

    private void Awake()
    {
        _instance = this;
    }

    public static void SpawnExplosion(Vector2 position)
    {
        _instance.ExplosionSound.PlayRandomPitch();

        var explosion = Instantiate(_instance.ExplosionPrefab, position, Quaternion.identity, _instance.transform);
        Destroy(explosion, 0.7f);
    }
}
