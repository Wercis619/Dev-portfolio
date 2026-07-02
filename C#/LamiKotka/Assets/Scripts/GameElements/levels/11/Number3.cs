using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number3 : MonoBehaviour
{
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            {
                enemy1.SetActive(true);

            }

        }
    }
}
