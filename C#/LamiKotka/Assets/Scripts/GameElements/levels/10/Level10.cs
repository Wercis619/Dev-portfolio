using System.Collections;
using UnityEngine;

public class Level10 : MonoBehaviour
{
    public Player player;
    public GameObject startCanvas; 
    public DragPlayer dragPlayer;  

    void Start()
    {
        dragPlayer.enabled = false;
        startCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            StartCoroutine(ShowStartCanvas());
        }
    }

    IEnumerator ShowStartCanvas()
    {
        startCanvas.SetActive(true);   
          
        yield return new WaitForSeconds(2f); 
        startCanvas.SetActive(false);  
        dragPlayer.enabled = true;     
    }
}
