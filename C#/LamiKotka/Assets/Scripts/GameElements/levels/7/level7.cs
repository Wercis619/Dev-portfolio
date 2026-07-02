using System.Collections;
using UnityEngine;


public class Level7 : MonoBehaviour
{
    public GameObject player; 
    public GameObject dialogCanvas; 
    public FireController fireController; 
    public Transform startMinigamePoint; 
    private bool canTriggerDialog = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && canTriggerDialog)
        {
            StartCoroutine(ShowDialog());
        }
    }

    private IEnumerator ShowDialog()
    {
        canTriggerDialog = false;

        
        if (dialogCanvas == null)
        {
            yield break; 
        }

        dialogCanvas.SetActive(true); 

        yield return new WaitForSeconds(3f); 

        dialogCanvas.SetActive(false); 

       
        if (fireController == null)
        {
            yield break; 
        }

        fireController.EnableBlowMechanic(); 
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
        canTriggerDialog = true; 
    }
}
