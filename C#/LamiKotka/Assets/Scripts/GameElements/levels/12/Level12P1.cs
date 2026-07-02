using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level12P1 : MonoBehaviour
{
    public GameObject player;
    public GameObject dialogCanvas;
    public Bat1 bat1;
    public Transform startMinigamePoint;
  

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (bat1.isScared == false)
            {
                StartCoroutine(ShowDialog());
                bat1.EnableScream();
            }
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            bat1.DisableScream();
        }
    }

    private IEnumerator ShowDialog()
    {
        dialogCanvas.SetActive(true);

        yield return new WaitForSeconds(5f);

        dialogCanvas.SetActive(false);
 
    }

    public void ResetPlayerPosition()
    {

        if (player == null)
        {
            return;
        }

        if (startMinigamePoint == null)
        {
            return;
        }

        player.transform.position = startMinigamePoint.position;
       
    }
}
