using UnityEngine;

public class PuzzleGameManager : MonoBehaviour
{
    public GameObject[] puzzlePieces;  // Tablica z puzzlami

    private PuzzleGame puzzleGame;

    void Start()
    {
        puzzleGame = FindObjectOfType<PuzzleGame>(); // Inicjalizujemy puzzleGame
    }

    public void CheckPuzzleCompletion()
    {
        foreach (GameObject piece in puzzlePieces)
        {
            Puzzle1Drag puzzleScript = piece.GetComponent<Puzzle1Drag>();
            float distance = Vector2.Distance(piece.transform.position, puzzleScript.GetTargetPosition());

            Debug.Log($"Piece Position: {piece.transform.position}, Target Position: {puzzleScript.GetTargetPosition()}, Distance: {distance}");

            if (distance > 0.1f)
            {
                return; // Jeœli chocia¿ jeden puzzel jest Ÿle ustawiony, nie koñczymy gry
            }
        }

        Debug.Log("Uk³adanka ukoñczona!");
        puzzleGame.CompletePuzzle();
    }
}
