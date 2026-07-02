using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class FireDamage : MonoBehaviour
{
    public Level7 level7; 
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.CompareTag("Player")) 
        {         
            if (collision.TryGetComponent(out Player player))
            {
                Debug.LogWarning("Kolizja z ogniem, zadawanie obra¿eñ.");
                player.TakeDamage("Fire");
                level7.ResetPlayerPosition(); 
            }
        }
    }
}
