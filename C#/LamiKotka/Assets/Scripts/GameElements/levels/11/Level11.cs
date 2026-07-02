using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level11 : MonoBehaviour
{
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
   [SerializeField] private GameObject bat;
    [SerializeField] private GameObject player;
   [SerializeField] private GameObject dialogCanvas;

    private void Start()
    {
        enemy1.SetActive(false);
        enemy2.SetActive(false);
        bat.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            
            {
                StartCoroutine(ShowDialog());
                
            }
        }
    }
    

    private IEnumerator ShowDialog()
    {
        dialogCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogCanvas.SetActive(false);
    }
}
