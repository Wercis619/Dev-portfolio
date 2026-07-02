using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantBullet : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private string groundTag = "Ground";
    private float bulletLifeSpan = 0f;

    private void Update()
    {
        float moveValue = movementSpeed * Time.deltaTime;
        transform.position += transform.right * moveValue;

        bulletLifeSpan += Time.deltaTime;
        if (bulletLifeSpan >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(groundTag))
        {
            Destroy(gameObject);

        }
        else if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage("Bullet");
            Destroy(gameObject);
        }
    }
}
