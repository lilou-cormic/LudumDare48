using PurpleCable;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _current = null;

    [SerializeField] float _Speed = 1.5f;
    public static float Speed => _current._Speed;

    [SerializeField] FishPool FishPool = null;

    [SerializeField] HealthPickupPool HealthPickupPool = null;

    [SerializeField] TorpedoPickupPool TorpedoPickupPool = null;

    [SerializeField] ShieldPickupPool ShieldPickupPool = null;

    [SerializeField] SpriteRenderer Background = null;

    [SerializeField] GameObject PausePanel = null;

    public static FishCollection FishCollection { get; private set; }

    private bool _IsPaused = false;
    private bool IsPaused
    {
        get => _IsPaused;

        set
        {
            _IsPaused = value;

            Time.timeScale = (_IsPaused ? 0 : 1);

            if (PausePanel != null)
                PausePanel.gameObject.SetActive(_IsPaused);
        }
    }

    public static bool IsGamePaused => _current.IsPaused;

    public static bool IsGameOver { get; internal set; }

    private float _timeLeft = 0;

    private void Awake()
    {
        _current = this;

        FishCollection = new FishCollection();
    }

    private void OnDestroy()
    {
        UnPause();

        _current = null;
    }

    private void Start()
    {
        IsPaused = false;
        IsGameOver = false;

        ScoreManager.ResetScore();

        InvokeRepeating(nameof(SpeedUp), 10, 10);

        InvokeRepeating(nameof(SpawnPickup), 5, 5);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") || (IsPaused && Input.GetButtonDown("Cancel")))
            IsPaused = !IsPaused;

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

        _Speed += 0.5f;

        Background.color = Color.HSVToRGB(0, 0, (1 - (_Speed - 1.5f) / 5));
    }

    private List<PoolableItem> ItemBag = new List<PoolableItem>();

    private void SpawnPickup()
    {
        if (IsGamePaused)
            return;

        if (Random.value >= 0.9f)
            return;

        if (ItemBag.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                HealthPickup healthPickup = HealthPickupPool.GetItem();
                healthPickup.gameObject.SetActive(false);

                ItemBag.Add(healthPickup);
            }

            for (int i = 0; i < 2; i++)
            {
                ShieldPickup shieldPickup = ShieldPickupPool.GetItem();
                shieldPickup.gameObject.SetActive(false);

                ItemBag.Add(shieldPickup);
            }

            for (int i = 0; i < 5; i++)
            {
                TorpedoPickup torpedoPickup = TorpedoPickupPool.GetItem();
                torpedoPickup.gameObject.SetActive(false);

                ItemBag.Add(torpedoPickup);
            }
        }

        PoolableItem item = ItemBag.ToArray().GetRandom();
        item.transform.position = new Vector3((Random.value * 5) - 2.5f, -7.5f, 0);
        ((IPoolable)item).SetAsInUse();
    }

    public void UnPause()
    {
        IsPaused = false;
    }
}
