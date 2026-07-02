using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bat2 : MonoBehaviour
{
    [Header("References")]
    public Level12P2 level12P2;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private Button musicOffButton;
    [SerializeField] private Button musicOnButton;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;

    private Vector3 rightPointPosition;
    private Vector3 leftPointPosition;
    private bool isMovingRight = true;
    private bool isStopped = false;
    private bool isCalmEnable = false;
   public bool isCalmed = false;

    private void Start()
    {
        leftPointPosition = leftPoint.position;
        rightPointPosition = rightPoint.position;

        if (musicOffButton != null)
        {
            musicOffButton.onClick.AddListener(OnMusicOffClicked);
        }

        if (musicOnButton != null)
        {
            musicOnButton.onClick.AddListener(OnMusicOnClicked);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (!isStopped)
            {
                player.TakeDamage("Bat2");
                level12P2.ResetPlayerPosition();
            }
        }
    }

    private void Update()
    {
        if (isCalmed)
        {
            StopMovement();
        }

        if (!isStopped)
        {
            Move();
        }
    }

    private void OnMusicOffClicked()
    {
        if (isCalmEnable && !isCalmed)
        {
            StartCoroutine(CalmABat());
        }
    }

    private void OnMusicOnClicked()
    {
        isStopped = false;
        isCalmed = false;
        animator.Play("fly2");
    }

    private IEnumerator CalmABat()
    {
        isCalmed = true;
        animator.SetTrigger("isNotFlying");
        yield return new WaitForSeconds(1f);
        animator.Play("Idle2");
    }

    private void Move()
    {
        float moveValue = movementSpeed * Time.deltaTime;
        if (isMovingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPointPosition, moveValue);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (transform.position == rightPointPosition)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPointPosition, moveValue);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (transform.position == leftPointPosition)
            {
                isMovingRight = true;
            }
        }
    }

    private void StopMovement()
    {
        isStopped = true;
    }

    public void EnableCalm()
    {
        isCalmEnable = true;
    }

    public void DisableCalm()
    {
        isCalmEnable = false;
    }
}
