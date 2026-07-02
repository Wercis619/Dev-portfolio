using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number2 : MonoBehaviour
{
    
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            {
                bat.SetActive(true);

            }

        }
    }
}
