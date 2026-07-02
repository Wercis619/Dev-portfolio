using UnityEngine;
using Cinemachine;

public class PuzzleGame : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcamPlayer;
    [SerializeField] private CinemachineVirtualCamera vcamPuzzle;
    [SerializeField] private GameObject godfish;

    public void EnableGame()
    {
        vcamPlayer.Priority = 0;
        vcamPuzzle.Priority = 10; // Prze³¹czamy na puzzlow¹ kamerê
    }

    public void CompletePuzzle()
    {
        vcamPlayer.Priority = 10;
        vcamPuzzle.Priority = 0; // Wracamy do gracza

        godfish.SetActive(true); // Aktywujemy godfish
    }
}
