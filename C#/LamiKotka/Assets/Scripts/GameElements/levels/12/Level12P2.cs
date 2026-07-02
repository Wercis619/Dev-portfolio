using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level12P2 : MonoBehaviour
{
    public GameObject player;
    public GameObject dialogCanvas;
    public Bat2 bat2;
    public Transform startMinigamePoint;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") )
        {
            if (bat2.isCalmed == false) 
            {
                StartCoroutine(ShowDialog());
                bat2.EnableCalm();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            bat2.DisableCalm();
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
