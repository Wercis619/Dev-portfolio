using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level16 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject dialogCanvas;
    private PlayerPuzzle playerPuzzle;
    private PuzzleUI puzzleUI;
    [SerializeField] private GameObject godfish;

    private bool hasCompleted = false;

    private void Start()
    {
        start.SetActive(false);
        playerPuzzle = FindObjectOfType<PlayerPuzzle>();
        puzzleUI = FindObjectOfType<PuzzleUI>();
        godfish.SetActive(false);

        if (playerPuzzle == null)
        {
            Debug.LogError("Nie znaleziono komponentu PlayerPuzzle!");
        }

        if (puzzleUI == null)
        {
            Debug.LogError("Nie znaleziono komponentu PuzzleUI!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& !hasCompleted)
        {
            if (playerPuzzle.GetCollectedPuzzle() < puzzleUI.puzzleOnMap)
            {
                StartCoroutine(ShowDialog());
            }
            else
            {
                hasCompleted = true;
                FindObjectOfType<PuzzleGame>().EnableGame();
                start.SetActive(true);
            }
        }
    }

    private IEnumerator ShowDialog()
    {
        dialogCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogCanvas.SetActive(false);
    }

}
