using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent(out Player player))
        {

            player.TakeDamage("Pool");
        }
        if (collision.TryGetComponent(out Pimpek pimpek))
        {
            pimpek.TakeDamage();
        }
    }
}
