using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private EnemyDamageCollider damageCollider;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private GameObject enemyParticlePrefab;
    [SerializeField] private float enemyParticleLifetime = 1f;

    [Header("Shooting")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject elephantBulletPrefab;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private float shootDelay = 3f;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private string shootTrigger;

    private float shootTimer = 0f;

    private void Start()
    {
        damageCollider.OnPlayerJumped += TakeDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage("Elephant");
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
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootDelay)
        {
            shootTimer -= shootDelay;
            Shoot();
        }
    }

    private void Shoot()
    {
        var spawnedBullet = Instantiate(elephantBulletPrefab, transform.position, Quaternion.identity);
        spawnedBullet.transform.right = -transform.right;

        if (FindObjectOfType<SoundManager>().AreSoundsOn)
        {
            AudioSource.PlayClipAtPoint(shootSound.clip, transform.position);
        }
        animator.SetTrigger(shootTrigger);
    }

}
