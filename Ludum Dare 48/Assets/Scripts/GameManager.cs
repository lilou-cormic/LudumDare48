using PurpleCable;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _current = null;

    [SerializeField] float _Speed = 1f;
    public static float Speed => _current._Speed;

    [SerializeField] FishPool FishPool = null;

    public static FishCollection FishCollection { get; private set; }

    public static bool IsGamePaused { get; internal set; }

    public static bool IsGameOver { get; internal set; }

    private float _timeLeft = 0;

    private void Awake()
    {
        _current = this;

        FishCollection = new FishCollection();
    }

    private void Start()
    {
        IsGamePaused = false;
        IsGameOver = false;

        ScoreManager.ResetScore();

        InvokeRepeating(nameof(SpeedUp), 10, 10f);
    }

    private void Update()
    {
        if (IsGamePaused)
            return;

        if (_timeLeft <= 0)
        {
            SpawnFish();

            if (!IsGameOver)
                ScoreManager.AddPoints(1);

            _timeLeft = 1 / Speed;
        }
        else
        {
            _timeLeft -= Time.deltaTime;
        }
    }

    private void SpawnFish()
    {
        if (Random.value > 0.4f)
            return;

        FishDef def = FishCollection.GetRandom();

        float x = (Random.value * 6) - 3;

        for (int i = 0; i < def.Count; i++)
        {
            Fish fish = FishPool.GetItem(def);
            fish.transform.position = new Vector3(x, -8, 0);

            x -= def.Spacing;
        }
    }

    private void SpeedUp()
    {
        if (IsGamePaused || IsGameOver)
            return;

        _Speed += 0.1f;
    }
}
