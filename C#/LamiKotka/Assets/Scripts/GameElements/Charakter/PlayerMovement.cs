


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GroundChecker groundChecker; // Odnoœnik do obiektu sprawdzaj¹cego grunt
    [SerializeField] private Rigidbody2D playerRigidbody; // Odnoœnik do Rigidbody2D gracza
    [SerializeField] private GameObject landingParticle; // Prefab cz¹steczek przy l¹dowaniu
    [SerializeField] private float landingParticleLifetime = 1f; // Czas ¿ycia cz¹steczek l¹dowania
    [SerializeField] private Transform legsTransform; // Transform nóg gracza
    [SerializeField] private GameObject movementParticlePrefab; // Prefab cz¹steczek ruchu
    [SerializeField] private float movementParticleLifetime = 0.3f; // Czas ¿ycia cz¹steczek ruchu

    [Space(5)]

    [Header("Settings")]
    [Range(300, 800)]
    [SerializeField] private float moveSpeed = 5.0f; // Prêdkoœæ ruchu
    [Range(7, 12)]
    [SerializeField] private float jumpPower = 8.0f; // Si³a skoku
    [Range(3, 9)]
    [SerializeField] private float doubleJumpPower = 8.0f; // Si³a podwójnego skoku

    [Space(5)]

    [Header("Sounds")]
    [SerializeField] private float moveSoundDelay = 0.1f; // OpóŸnienie dŸwiêku ruchu
    [SerializeField] private AudioSource jumpSound; // DŸwiêk skoku
    [SerializeField] private AudioSource moveSound; // DŸwiêk ruchu

    private float moveSoundTimer = 0f; // Timer do dŸwiêku ruchu
    private float inputX = 0f; // Wejœcie w osi X
    private bool isJumpingInput = false; // Flaga wejœcia skoku
    private bool isDoubleJump = false; // Flaga podwójnego skoku

    private Platform currentPlatform; // Aktualna platforma, na której stoi gracz
    private bool isFrozen = false; // Flaga zamra¿aj¹ca ruch

    private void Start()
    {
        groundChecker.OnLanding += HandleLanding; // Pod³¹czenie metody obs³ugi l¹dowania
    }

    private void HandleLanding()
    {
        // Resetuj flagi skakania po l¹dowaniu
        ResetJumpState(); // Resetuj stan skoku
        if (!isFrozen)
        {
            var spawnedPrefab = Instantiate(landingParticle, legsTransform.position, Quaternion.identity);
            Destroy(spawnedPrefab, landingParticleLifetime);
        }
    }

    private void FixedUpdate()
    {
        if (isFrozen) return; // Zatrzymaj wszystkie aktualizacje ruchu, jeœli gracz jest zamro¿ony

        HandleMovementEffect(); // Efekty ruchu

        // P³ynny ruch
        float moveInput = inputX * moveSpeed * Time.fixedDeltaTime;
        playerRigidbody.velocity = new Vector2(moveInput, playerRigidbody.velocity.y); // Ustaw prêdkoœæ w osi X

        // SprawdŸ skok
        if (isJumpingInput)
        {
            Jump(); // Wykonaj skok
        }
    }

    private void Jump()
    {
        if (groundChecker.isGrounded)
        {
            // Skok z ziemi
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0); // Resetuj prêdkoœæ Y
            playerRigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            isJumpingInput = false; // Resetuj wejœcie skoku
            isDoubleJump = true; // Umo¿liw podwójny skok
            FindObjectOfType<SoundManager>().PlaySound(jumpSound); // Odtwórz dŸwiêk skoku
        }
        else if (isDoubleJump)
        {
            // Podwójny skok
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0); // Resetuj prêdkoœæ Y
            playerRigidbody.AddForce(new Vector2(0, doubleJumpPower), ForceMode2D.Impulse);
            isJumpingInput = false; // Resetuj wejœcie skoku
            isDoubleJump = false; // Resetuj flagê podwójnego skoku
            FindObjectOfType<SoundManager>().PlaySound(jumpSound); // Odtwórz dŸwiêk skoku
        }
    }

    public void Jump1()
    {
        if (isFrozen) return; // Zablokuj skoki, jeœli gracz jest zamro¿ony
        isJumpingInput = true; // Ustaw wejœcie skoku
    }

    public void Crouch()
    {
        if (isFrozen) return; // Zablokuj kucanie, jeœli gracz jest zamro¿ony

        if (currentPlatform != null && groundChecker.isGrounded)
        {
            currentPlatform.SetCollidable(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            currentPlatform = platform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            currentPlatform = null;
        }
    }

    private void HandleMovementEffect()
    {
        if (Mathf.Abs(inputX) > Mathf.Epsilon && groundChecker.isGrounded)
        {
            moveSoundTimer += Time.deltaTime;
            if (moveSoundTimer >= moveSoundDelay)
            {
                FindObjectOfType<SoundManager>().PlaySound(moveSound); // Odtwórz dŸwiêk ruchu

                moveSoundTimer -= moveSoundDelay;
                var spawnedPrefab = Instantiate(movementParticlePrefab, legsTransform.position, Quaternion.identity);
                Destroy(spawnedPrefab, movementParticleLifetime); // Zniszcz cz¹steczki po czasie ¿ycia
            }
        }
    }

    public void StartMovingLeft()
    {
        if (isFrozen) return; // Zablokuj ruch w lewo, jeœli gracz jest zamro¿ony
        inputX = -1f;  // Ustaw kierunek na lewo
    }

    public void StopMovingLeft()
    {
        if (inputX < 0f)
            inputX = 0f;  // Zatrzymaj ruch w lewo
    }

    public void StartMovingRight()
    {
        if (isFrozen) return; // Zablokuj ruch w prawo, jeœli gracz jest zamro¿ony
        inputX = 1f;  // Ustaw kierunek na prawo
    }

    public void StopMovingRight()
    {
        if (inputX > 0f)
            inputX = 0f;  // Zatrzymaj ruch w prawo
    }

    public bool IsMoving()
    {
        return Mathf.Abs(inputX) > Mathf.Epsilon; // SprawdŸ, czy gracz siê porusza
    }

    public void FreezeMovement()
    {
        isFrozen = true; // Zamro¿enie gracza
        playerRigidbody.velocity = Vector2.zero; // Zatrzymanie ruchu gracza
    }

    public void UnfreezeMovement()
    {
        isFrozen = false; // Odmro¿enie gracza
    }

    // Nowa metoda zwracaj¹ca aktualny stan wejœcia X
    public float GetCurrentInputX()
    {
        return inputX; // Zwróæ aktualny stan wejœcia X
    }

    // Nowa metoda resetuj¹ca stan skoku
    public void ResetJumpState()
    {
        isDoubleJump = false; // Resetuj stan podwójnego skoku
    }
}
