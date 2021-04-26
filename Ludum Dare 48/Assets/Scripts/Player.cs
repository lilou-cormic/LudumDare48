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

    [SerializeField] GameObject Electricity = null;

    [SerializeField] AudioClip GameOverSound = null;

    [SerializeField] AudioClip HurtSound = null;

    [SerializeField] AudioClip ShootSound = null;

    public int TorpedoCount { get; set; } = 3;

    private Health health = null;

    private bool _isDead = false;

    private bool _isGettingHit = false;

    private int _maxTorpedoCount = 5;

    public event Action TorpedoCountChanged;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.HPDepleted += Health_HPDepleted;

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
            GetHit();
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

    private void GetHit()
    {
        if (_isDead || _isGettingHit)
            return;

        StartCoroutine(DoGetHit());

        StartCoroutine(DoFlash());
    }

    private IEnumerator DoGetHit()
    {
        _isGettingHit = true;

        HurtSound.Play();

        health.ChangeHP(-1);

        SpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(1f);

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
        StartCoroutine(DoElectrify());
    }

    private IEnumerator DoElectrify()
    {
        Electricity.SetActive(true);

        yield return new WaitForSeconds(1f);

        Electricity.SetActive(false);
    }

    private void Health_HPDepleted(Health health)
    {
        Die();
    }
}
