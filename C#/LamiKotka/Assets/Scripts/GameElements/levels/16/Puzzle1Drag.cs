using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Drag : MonoBehaviour
{
    [SerializeField] private Transform target; // Przypisz w Inspectorze obiekt docelowy
    private Vector2 startPosition;
    private bool isDragging = false;
    private Vector2 offset;
    public float snapTolerance = 0.5f; // Maksymalna odleg³oœæ do snapowania
    private Vector2 targetPosition; // Docelowa pozycja puzzla

    void Start()
    {
        startPosition = transform.position; // Zapamiêtaj pozycjê startow¹
        if (target != null)
        {
            targetPosition = target.position; // Ustaw targetPosition na pozycjê obiektu target
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = newPosition;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance <= snapTolerance)
            {
                transform.position = target.position; // Snap do docelowej pozycji
                targetPosition = target.position; // Ustaw targetPosition na docelow¹ pozycjê
            }
            else
            {
                transform.position = startPosition; // Powrót do pozycji pocz¹tkowej
            }
        }
        // Wywo³anie CheckPuzzleCompletion po zakoñczeniu przeci¹gania
        FindObjectOfType<PuzzleGameManager>().CheckPuzzleCompletion();
    }

    // Metoda, aby zwróciæ aktualn¹ docelow¹ pozycjê
    public Vector2 GetTargetPosition()
    {
        return targetPosition; // Zwróæ aktualn¹ docelow¹ pozycjê
    }
}
