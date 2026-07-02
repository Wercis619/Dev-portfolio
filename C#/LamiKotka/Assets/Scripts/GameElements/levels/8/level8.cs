using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class level8 : MonoBehaviour
{
    public GameObject player; 
    public GameObject dialogCanvas; 
    public GameObject secondDialogCanvas; 
    public Transform ResetPosition;

    public Transform firstPoint; 
    public Transform secondPoint; 

    public float firstPointRange = 1.5f; 
    public float secondPointRange = 3.0f; 

    public Planks8 planks; 
    public Crate8 crate; 

    private bool firstDialogShown = false; 
    private bool secondDialogShown = false;
    private bool crateMechanicEnabled = false; 

    private void Start()
    {
        crate.DisableThrowMechanic();
        planks.DisablePlanksMechanic();
    }

    private void Update()
    {
        if (firstDialogShown && !secondDialogShown && Vector3.Distance(player.transform.position, firstPoint.position) <= firstPointRange)
        {
            StartCoroutine(ShowSecondDialog());
        }

        if (!crateMechanicEnabled && Vector3.Distance(player.transform.position, secondPoint.position) <= secondPointRange && secondDialogShown)
        {
            crate.EnableThrowMechanic(); 
            crateMechanicEnabled = true; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!planks.IsFirstPlankPlaced())
            {
                StartCoroutine(ShowDialog());
                planks.EnablePlanksMechanic();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Nie wy³¹czamy mechaniki desek, jeœli choæ jedna zosta³a u³o¿ona
            if (!planks.IsFirstPlankPlaced())
            {
                planks.DisablePlanksMechanic();
            }
        }
    }

    private IEnumerator ShowDialog()
    {
        if (dialogCanvas == null)
        {
            yield break;
        }
        dialogCanvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        dialogCanvas.SetActive(false);
        firstDialogShown = true;
    }

    private IEnumerator ShowSecondDialog()
    {
        if (secondDialogCanvas == null)
        {
            yield break;
        }
        secondDialogCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        secondDialogCanvas.SetActive(false);
        secondDialogShown = true;
    }

    public void ResetPlayerPosition()
    {
        if (player == null || ResetPosition == null)
        {
            return;
        }

        player.transform.position = ResetPosition.position;
    }

    public void ResetSetings()
    {
        firstDialogShown = false; 
        secondDialogShown = false;
        crateMechanicEnabled = false;
    }
}
