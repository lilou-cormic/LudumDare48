using PurpleCable;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer = null;

    private Health health = null;

    private bool _isDead = false;

    private bool _isGettingHit = false;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.HPDepleted += Health_HPDepleted;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

        ScoreManager.SetHighScore();

        yield return new WaitForSeconds(1f);

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


    private void Health_HPDepleted(Health health)
    {
        Die();
    }
}
