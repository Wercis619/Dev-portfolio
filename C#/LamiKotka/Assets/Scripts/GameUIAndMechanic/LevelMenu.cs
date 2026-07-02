using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button[] levelButtons;
   

    //dodanie blokowania leveli poza pierwszym i odblokowanie
    private void Start()
    {
        Debug.Log($"Iloœæ przycisków poziomów: {levelButtons.Length}");
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1); // Domyœlnie 1 dostêpny poziom

        for (int i = 0; i < levelButtons.Length; i++)
        {
            // int levelIndex = i + 3; // Indeks sceny w Build Settings (poziomy zaczynaj¹ siê od indeksu 3)
            int levelIndex = i + 3;
            if (levelIndex >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogError($"Level {levelIndex} przekracza iloœæ scen w Build Settings!");
                continue;
            }

            if (i + 1 <= unlockedLevels) // Odblokowane poziomy
            {
                levelButtons[i].interactable = true;
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
                UpdateMedalsForLevel(i);
            }
            else // Zablokowane poziomy
            {
                levelButtons[i].interactable = false;
                SetButtonColor(levelButtons[i], Color.gray);
            }
        }

        backToMenuButton.onClick.AddListener(BackToMenu);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(int levelIndex)
    {
        GameManager.Instance.LoadLevel(levelIndex);
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.disabledColor = color;
        button.colors = cb;
    }

    private void UpdateMedalsForLevel(int indexInArray)
    {
        Debug.Log($"Sprawdzam medale dla poziomu: {indexInArray}");
        string levelName = $"Level{indexInArray + 1}";

        Transform canvasTransform = transform.Find("Canvas");
        if (canvasTransform == null)
        {
            Debug.LogWarning("Nie znaleziono obiektu Canvas w hierarchii!");
            return;
        }

        Transform levelTransform = canvasTransform.Find(levelName);
        if (levelTransform == null)
        {
            Debug.LogWarning($"Nie znaleziono poziomu: {levelName}");
            return;
        }

        Transform goldMedal = levelTransform.Find("gold");
        Transform silverMedal = levelTransform.Find("silver");
        Transform bronzeMedal = levelTransform.Find("bronze");

        if (goldMedal == null || silverMedal == null || bronzeMedal == null)
        {
            Debug.LogError($"Brakuje medali dla poziomu: {levelName}");
            return;
        }

        goldMedal.gameObject.SetActive(false);
        silverMedal.gameObject.SetActive(false);
        bronzeMedal.gameObject.SetActive(false);

        int stars = GameManager.Instance.GetBestStarsForLevel(indexInArray + 3);


        switch (stars)
        {
            case 0:
                break;
            case 1:
                bronzeMedal.gameObject.SetActive(true);
                break;
            case 2:
                silverMedal.gameObject.SetActive(true);
                break;
            case 3:
                goldMedal.gameObject.SetActive(true);
                break;
            default:
                Debug.LogError($"Nieprawid³owa liczba gwiazdek: {stars}");
                break;
        }
    }
}


