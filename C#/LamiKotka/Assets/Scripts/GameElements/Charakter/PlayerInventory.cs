using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int miceCounter = 0;
    private int goldfishCounter = 0;

    public void AddMouse()
    {
        GameManager.Instance.AddCollectedMouse();
        Debug.Log("Mice collected: " + GameManager.Instance.GetCollectedMice());
    }

  

    public void AddFish()
    {
        goldfishCounter += 1;
        Debug.Log("Fish collected: " + goldfishCounter);
    }

    public int GetCollectedMice()
    {
        return miceCounter;
    }

    public int GetCollectedGoldfish()
    {
        return goldfishCounter;
    }

}
