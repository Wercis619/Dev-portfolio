using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int CurrentLevelIndex { get; private set; }

    private int totalMiceOnLevel = 0;
    private int collectedMice = 0;

    private Dictionary<int, int> bestStarsPerLevel = new Dictionary<int, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadStars();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /* public void LoadStars()
     {
         int totalNumberOfLevels = 16;
         for (int i = 0; i < totalNumberOfLevels; i++)
         {
             int stars = PlayerPrefs.GetInt($"StarsLevel{i}", 0);
             bestStarsPerLevel[i] = stars;
         }
     }*/
    public void LoadStars()
    {
        int totalNumberOfLevels = 16;
        for (int i = 0; i < totalNumberOfLevels; i++)
        {
            int stars = PlayerPrefs.GetInt($"StarsLevel{i + 3}", 0);
            bestStarsPerLevel[i + 3] = stars;
        }
    }

    public void ResetAllData()
    {
        bestStarsPerLevel.Clear();
        PlayerPrefs.DeleteAll();
    }

    public void SetCurrentLevel(int levelIndex)
    {
        CurrentLevelIndex = levelIndex;
    }

    public void LoadLevel(int levelIndex)
    {
        SetCurrentLevel(levelIndex);
        ResetLevelData();
        SceneManager.LoadScene(levelIndex);
    }

    public void RestartLevel()
    {
        ResetLevelData();
        SceneManager.LoadScene(CurrentLevelIndex);
    }

    //dodana metoda odblokowania levelu
    public void UnlockNextLevel()
    {
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
        if (CurrentLevelIndex - 2 >= unlockedLevels)
        {
            PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels + 1);
            PlayerPrefs.Save();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetTotalMice(int miceCount)
    {
        totalMiceOnLevel = miceCount;
    }

    public int GetTotalMice()
    {
        return totalMiceOnLevel;
    }

    public void AddCollectedMouse()
    {
        collectedMice++;
    }

    public int GetCollectedMice()
    {
        return collectedMice;
    }

    public void ResetLevelData()
    {
        collectedMice = 0;
    }

    /* public void SaveStarsForLevel(int levelIndex, int stars)
     {
         if (bestStarsPerLevel.ContainsKey(levelIndex))
         {
             if (bestStarsPerLevel[levelIndex] < stars)
             {
                 bestStarsPerLevel[levelIndex] = stars;
             }
         }
         else
         {
             bestStarsPerLevel.Add(levelIndex, stars);
         }

         PlayerPrefs.SetInt($"StarsLevel{levelIndex}", stars);
         PlayerPrefs.Save();
     } */

    /* public void SaveStarsForLevel(int levelIndex, int stars)
     {
         if (bestStarsPerLevel.ContainsKey(levelIndex))
         {
             if (bestStarsPerLevel[levelIndex] < stars) // Zapisuje tylko lepszy wynik
             {
                 bestStarsPerLevel[levelIndex] = stars;
                 PlayerPrefs.SetInt($"StarsLevel{levelIndex}", stars);
                 PlayerPrefs.Save();
             }
         }
         else
         {
             bestStarsPerLevel[levelIndex] = stars;
             PlayerPrefs.SetInt($"StarsLevel{levelIndex}", stars);
             PlayerPrefs.Save();
         }
     } */
    public void SaveStarsForLevel(int levelIndex, int stars)
    {
        Debug.Log($"🔹 Zapisuję {stars} gwiazdek dla StarsLevel{levelIndex}");

        if (bestStarsPerLevel.ContainsKey(levelIndex))
        {
            if (bestStarsPerLevel[levelIndex] < stars)
            {
                bestStarsPerLevel[levelIndex] = stars;
                PlayerPrefs.SetInt($"StarsLevel{levelIndex}", stars);
                PlayerPrefs.Save();
            }
        }
        else
        {
            bestStarsPerLevel[levelIndex] = stars;
            PlayerPrefs.SetInt($"StarsLevel{levelIndex}", stars);
            PlayerPrefs.Save();
        }
    }



    /* public int GetBestStarsForLevel(int levelIndex)
     {
         if (bestStarsPerLevel.ContainsKey(levelIndex))
         {
             return bestStarsPerLevel[levelIndex];
         }
         else
         {
             return 0;
         }
     }*/
    public int GetBestStarsForLevel(int levelIndex)
    {
        if (bestStarsPerLevel.ContainsKey(levelIndex))
        {
            Debug.Log($"🔹 Pobieram {bestStarsPerLevel[levelIndex]} gwiazdek dla StarsLevel{levelIndex}");
            return bestStarsPerLevel[levelIndex];
        }
        else
        {
            Debug.Log($"⚠ Brak gwiazdek dla StarsLevel{levelIndex}");
            return 0;
        }
    }

}


