using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject dialogBox;

    [Header("Goldfish Dragging")]
    [SerializeField] private GameObject goldfishimage; 
    [SerializeField] private GameObject goldfish; 
    [SerializeField] private Transform player;    
    [SerializeField] private PlayerMovement movement;

    [Header("Settings")]
    [SerializeField] private float dialogDuration = 3f;

    private bool hasDialogBeenShown = false;

    private void Start()
    {
        goldfish.SetActive(false);
        goldfishimage.SetActive(false);

        FishDrag fishDrag = goldfish.GetComponent<FishDrag>();
        if (fishDrag != null)
        {
            fishDrag.SetPlayer(player); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDialogBeenShown)
        {
            ShowDialog();
            StartCoroutine(FreezePlayerAfterDelay(0.2f)); 
        }
    }
    private IEnumerator FreezePlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        movement.FreezeMovement(); 
    }

    public void ShowDialog()
    {
        dialogBox.SetActive(true);
        StartCoroutine(HideDialogAfterDelay(dialogDuration));
        hasDialogBeenShown = true;

        goldfishimage.SetActive(true);

        FishDrag fishDrag = goldfish.GetComponent<FishDrag>();
        if (fishDrag != null)
        {
            fishDrag.SetInitialPosition(); 
            fishDrag.EnableDragging();    
        }
    }

    public void ImagePointed()
    {
        goldfish.SetActive(true);
    }

    private IEnumerator HideDialogAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogBox.SetActive(false);
    }
}




