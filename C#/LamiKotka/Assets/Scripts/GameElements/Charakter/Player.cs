
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource waterSound;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerAnimations playerAnimations;
    [SerializeField] private PlayerMovement movement;

    [SerializeField] private int maxLives = 3;

    private int currentLives;
    private Vector3 startPosition;
    private bool isInvulnerable = false;

    private void Start()
    {
        startPosition = transform.position;
        currentLives = maxLives;

        GameManager.Instance.SetCurrentLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeDamage(string source)
    {
        if (isInvulnerable) return;

        playerAnimations.PlayHitAnimation();

        if (source == "Pool")
        {
            if (FindObjectOfType<SoundManager>().AreSoundsOn)
            {
                StartCoroutine(PlayPoolSounds());
            }
        }

        else if (source == "Lake")
        {
            if (FindObjectOfType<SoundManager>().AreSoundsOn)
            {
                StartCoroutine(PlayPoolSounds());
            }
        }
        else
        {
            if (FindObjectOfType<SoundManager>().AreSoundsOn)
            {
                AudioSource.PlayClipAtPoint(hitSound.clip, transform.position);
            }
        }

        currentLives--;

        

        if (currentLives <= 0)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            StartCoroutine(HandleDamage(source));
        }
    }

    private IEnumerator HandleDamage(string source)
    {
        isInvulnerable = true;

        if (source == "Snail")
        {
            SnailEnemy snailEnemy = FindObjectOfType<SnailEnemy>();
            if (snailEnemy != null)
            {
                snailEnemy.StopForSeconds();
            }
            movement.FreezeMovement();

            yield return new WaitForSeconds(0.5f);
            ResetPosition();
            ResetPlayerState();
            movement.UnfreezeMovement();
            isInvulnerable = false;
        }

        else if (source == "Pool")
        {
            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPosition();
            ResetPlayerState();
            isInvulnerable = false;
        }

        else if (source == "Elephant")
        {
            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPosition();
            ResetPlayerState();
            isInvulnerable = false;
        }

        else if (source == "Bullet")
        {

            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPosition();
            ResetPlayerState();
            isInvulnerable = false;
        }

        else if (source == "Fire")
        {
            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPlayerState();
            isInvulnerable = false;
        }

        else if (source == "Lake")
        {
            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPlayerState();
            isInvulnerable = false;
        }

        else if (source == "Azor")
        {
            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPosition();
            ResetPlayerState();
            isInvulnerable = false;
        }

        else if (source == "Bat1")
        {
            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPlayerState();
            isInvulnerable = false;
        }
        else if (source == "Bat2")
        {
            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPlayerState();
            isInvulnerable = false;
        }

        else if (source == "Snowman")
        {
            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPosition();
            ResetPlayerState();
            isInvulnerable = false;
        }

        else if (source == "SnowBullet")
        {

            movement.FreezeMovement();

            yield return new WaitForSeconds(1f);
            movement.UnfreezeMovement();
            ResetPosition();
            ResetPlayerState();
            isInvulnerable = false;
        }

        if (source == "Husky")
        {
            Husky husky = FindObjectOfType<Husky>();
            if (husky != null)
            {
                husky.StopForSeconds();
            }
            movement.FreezeMovement();

            yield return new WaitForSeconds(0.5f);
            ResetPosition();
            ResetPlayerState();
            movement.UnfreezeMovement();
            isInvulnerable = false;
        }

    }

    private IEnumerator PlayPoolSounds()
    {
        AudioSource.PlayClipAtPoint(waterSound.clip, transform.position);
        yield return new WaitForSeconds(0.6f);

        AudioSource.PlayClipAtPoint(hitSound.clip, transform.position);
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        playerRigidbody.velocity = Vector2.zero;
    }


    private void ResetPlayerState()
    {
        var playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ResetJumpState(); // Przywrócenie logiki skoku
        }
    }

    public void AddForce(float force)
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
        playerRigidbody.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
    }

    public int GetMaxLives()
    {
        return maxLives;
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }
}


