using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnailEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private EnemyDamageCollider damageCollider;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private GameObject enemyParticlePrefab;
    [SerializeField] private float enemyParticleLifetime = 1f;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;

    private Vector3 rightPointPosition;
    private Vector3 leftPointPosition;

    private bool isMovingRight = true;
    private bool isStopped = false; 
    private const float stopTime = 1.6f; 

    private void Start()
    {
        leftPointPosition = leftPoint.position;
        rightPointPosition = rightPoint.position;

        damageCollider.OnPlayerJumped += TakeDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage("Snail");
        }
        if (collision.TryGetComponent(out Pimpek pimpek))
        {
            pimpek.TakeDamage();
        }
    }
   

    private void TakeDamage()
    {
        if (FindObjectOfType<SoundManager>().AreSoundsOn)
        {
            AudioSource.PlayClipAtPoint(damageSound.clip, transform.position);
        }

        var spawnedPrefab = Instantiate(enemyParticlePrefab, transform.position, Quaternion.identity);
        Destroy(spawnedPrefab, enemyParticleLifetime);

        Destroy(gameObject);
    }

    private void Update()
    {
        if (!isStopped) 
        {
            Move();
        }
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

    public void StopForSeconds()
    {
        StartCoroutine(StopMovement()); 
    }

    private IEnumerator StopMovement()
    {
        Debug.Log("Œlimak zatrzymany."); 
        isStopped = true; 
        yield return new WaitForSeconds(stopTime); 
        isStopped = false; 
        Debug.Log("Œlimak wznowi³ ruch."); 
    }
}
