using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number4 : MonoBehaviour
{
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            {
                enemy2.SetActive(true);

            }

        }
    }
}
