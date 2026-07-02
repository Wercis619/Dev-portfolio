using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleUI : MonoBehaviour
{
    [Header("Puzzle Counter")]
    [SerializeField] private TextMeshProUGUI puzzleCounterText;

    private Player player;
    private PlayerPuzzle playerPuzzle;

    public int puzzleOnMap;

    private void Awake()
    {

        playerPuzzle = FindObjectOfType<PlayerPuzzle>();
        player = FindObjectOfType<Player>();

        if (playerPuzzle == null || player == null)
        {
            Debug.LogError("Nie znaleziono Player lub PlayerPuzzle!");
        }

        var puzzle = FindObjectsOfType<Puzzle>();
        puzzleOnMap = puzzle.Length;

    }

    private void Update()
    {
        puzzleCounterText.text = $"{playerPuzzle.GetCollectedPuzzle()}/{puzzleOnMap}";
    }

}
