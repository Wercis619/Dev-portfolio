using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level13 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dialogCanvas;
    [SerializeField] private GameObject snowball1;
    [SerializeField] private GameObject snowball2;
    [SerializeField] private GameObject snowball3;
    private bool hasCompleted = false; 

    private void Start()
    {
        snowball1.SetActive(false);
        snowball2.SetActive(false);
        snowball3.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasCompleted)
        {
            StartCoroutine(ShowDialog());
            snowball1.SetActive(true);
            snowball2.SetActive(true);
            snowball3.SetActive(true);
        }
    }

    private IEnumerator ShowDialog()
    {
        dialogCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogCanvas.SetActive(false);
    }

    public void MarkAsCompleted()
    {
        hasCompleted = true; 
    }
}
