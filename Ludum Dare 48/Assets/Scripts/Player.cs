using PurpleCable;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer = null;

    [SerializeField] TorpedoPool TorpedoPool = null;

    [SerializeField] Transform TorpedoSpawnPoint = null;

    [SerializeField] GameObject Shield = null;

    [SerializeField] GameObject Electricity = null;

    [SerializeField] AudioClip GameOverSound = null;

    [SerializeField] AudioClip HurtSound = null;

    [SerializeField] AudioClip ShootSound = null;

    [SerializeField] AudioClip ShieldDownSound = null;

    public int TorpedoCount { get; set; } = 3;

    private Health health = null;

    private bool _isDead = false;

    private bool _isGettingHit = false;

    private int _maxTorpedoCount = 5;

    private bool _isShieldUp = false;

    public event Action TorpedoCountChanged;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.HPDepleted += Health_HPDepleted;

        Shield.SetActive(false);
        Electricity.SetActive(false);
    }

    private void OnDestroy()
    {
        health.HPDepleted -= Health_HPDepleted;
    }

    private void Update()
    {
        if (_isDead || GameManager.IsGamePaused)
            return;

        var horizontal = Input.GetAxisRaw("Horizontal");

        MoveController.Move(transform, horizontal, 8);

        if (transform.position.x > 2.2f)
            transform.position = new Vector3(2.2f, transform.position.y, 0);
        else if (transform.position.x < -2.2f)
            transform.position = new Vector3(-2.2f, transform.position.y, 0);

        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            GetHit(collision.GetComponent<ElectrifyOnTouch>() != null || collision.GetComponent<ExplodeOnTouch>() != null ? 2f : 1f);
    }

    private void Die()
    {
        if (_isDead)
            return;

        _isDead = true;

        StartCoroutine(DoDie());
    }

    private IEnumerator DoDie()
    {
        GameManager.IsGameOver = true;

        yield return new WaitForSeconds(0.2f);

        GameOverSound.Play();

        ScoreManager.SetHighScore();

        yield return new WaitForSeconds(0.8f);

        SceneManager.LoadScene("GameOver");
    }

    private void GetHit(float duration)
    {
        if (_isDead || _isGettingHit)
            return;

        if (_isShieldUp)
        {
            StartCoroutine(DoLoseShield());
        }
        else
        {
            StartCoroutine(DoGetHit(duration));

            StartCoroutine(DoFlash());
        }
    }

    private IEnumerator DoLoseShield()
    {
        _isGettingHit = true;

        _isShieldUp = false;

        ShieldDownSound.Play();

        for (float f = 1; f >= 0; f -= 0.2f)
        {
            Shield.transform.localScale = new Vector3(f, f, f);

            yield return new WaitForSeconds(0.05f);
        }

        Shield.SetActive(false);

        _isGettingHit = false;
    }


    private IEnumerator DoGetHit(float duration)
    {
        _isGettingHit = true;

        HurtSound.Play();

        health.ChangeHP(-1);

        SpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(duration);

        SpriteRenderer.color = Color.white;

        _isGettingHit = false;
    }

    private IEnumerator DoFlash()
    {
        while (_isGettingHit)
        {
            SpriteRenderer.enabled = true;

            yield return new WaitForSeconds(0.1f);

            SpriteRenderer.enabled = false;

            yield return new WaitForSeconds(0.1f);
        }

        SpriteRenderer.enabled = true;
    }

    private void Shoot()
    {
        if (TorpedoCount == 0)
            return;

        ShootSound.Play();

        Torpedo torpedo = TorpedoPool.GetItem();
        torpedo.transform.position = TorpedoSpawnPoint.transform.position;

        TorpedoCount--;

        TorpedoCountChanged.Invoke();
    }

    public void AddTorpedo()
    {
        if (TorpedoCount < _maxTorpedoCount)
            TorpedoCount++;

        TorpedoCountChanged.Invoke();
    }

    public void Electrify()
    {
        if (_isShieldUp)
            return;

        StartCoroutine(DoElectrify());
    }

    private IEnumerator DoElectrify()
    {
        Electricity.SetActive(true);

        yield return new WaitForSeconds(1f);

        Electricity.SetActive(false);
    }

    public void ActivateShield()
    {
        if (_isShieldUp)
            return;

        _isShieldUp = true;

        StartCoroutine(DoActivateShield());
    }

    private IEnumerator DoActivateShield()
    {
        Shield.SetActive(true);

        for (float f = 0; f <= 1; f += 0.2f)
        {
            Shield.transform.localScale = new Vector3(f, f, f);

            yield return new WaitForSeconds(0.005f);
        }
    }

    public bool HasShieldDown() => !_isShieldUp;

    private void Health_HPDepleted(Health health)
    {
        Die();
    }
}