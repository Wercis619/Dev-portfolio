using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level9 : MonoBehaviour
{
    public GameObject player;  
    public GameObject startGameCanvas;  
    public GameObject secondCanvas;  
    public GameObject boneUI;  
    public bool hasBone = false;  
    private bool secondDialogShown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (!hasBone)
            {
                startGameCanvas.SetActive(true); 
            }
            else
            {
                if (secondDialogShown==false) 
                {
                    secondCanvas.SetActive(true); 
                    secondDialogShown = true;
                }
               
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (!hasBone)
            {
                startGameCanvas.SetActive(false); 
            }
            else
            {
                secondCanvas.SetActive(false); 
            }
        }
    }

    public void PickUpBone()
    {
        hasBone = true;
        boneUI.SetActive(true); 
    }
}
