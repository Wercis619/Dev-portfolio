using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Level14 : MonoBehaviour
{ 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dialogCanvas;
    [SerializeField] private GameObject denmarkBall;
    [SerializeField] private GameObject swedenBall;
    [SerializeField] private GameObject norwayBall;
    [SerializeField] private GameObject finlandBall;
    [SerializeField] private GameObject icelandBall;
    [SerializeField] private GameObject faroeIslandBall;
   
    private bool hasCompleted = false;

    private void Start()
    {
        denmarkBall.SetActive(false);
        swedenBall.SetActive(false);
        norwayBall.SetActive(false);
        finlandBall.SetActive(false);
        icelandBall.SetActive(false);
        faroeIslandBall.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasCompleted)
        {
            player.GetComponent<PlayerMovement>().FreezeMovement();
            StartCoroutine(ShowDialog());
            denmarkBall.SetActive(true);
            swedenBall.SetActive(true);
            norwayBall.SetActive(true);
            finlandBall.SetActive(true);
            icelandBall.SetActive(true);
            faroeIslandBall.SetActive(true);
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
        player.GetComponent<PlayerMovement>().UnfreezeMovement();
    }
}