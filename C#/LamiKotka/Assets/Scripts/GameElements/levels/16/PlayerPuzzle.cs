using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPuzzle : MonoBehaviour
{
    private int puzzleCounter = 0;
   

    public void AddPuzzle()
    {
        puzzleCounter += 1;
        Debug.Log("Puzzle collected: " + puzzleCounter);
    }


    public int GetCollectedPuzzle()
    {
        return puzzleCounter;
    }
}
