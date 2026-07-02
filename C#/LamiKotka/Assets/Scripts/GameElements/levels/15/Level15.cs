using System.Collections;
using UnityEngine;

public class Level15 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dialogCanvas;
    [SerializeField] private GameObject dialogCanvas2;
    private bool hasCompleted = false;
    public bool snowballPlaced = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !snowballPlaced)
        {
            if (GameManager.Instance.GetCollectedMice() < GameManager.Instance.GetTotalMice())
            {
                StartCoroutine(ShowDialog());
            }
            else 
            {
                hasCompleted = true;
                StartCoroutine(ShowDialog2());
                FindObjectOfType<DragSnowball>().EnableDragging();
            }
        }
    }

    private IEnumerator ShowDialog()
    {
        dialogCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogCanvas.SetActive(false);
    }

    private IEnumerator ShowDialog2()
    {
        dialogCanvas2.SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogCanvas2.SetActive(false);
    }
}