using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    [SerializeField] private float playerJumpPower = 5f;
    public event Action OnPlayerJumped;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent(out Player player))
        {

            OnPlayerJumped?.Invoke();
            player.AddForce(playerJumpPower);

        }
    }
}
