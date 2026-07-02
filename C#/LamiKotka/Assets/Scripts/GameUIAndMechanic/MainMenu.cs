using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button newGameButton;

    private void Start()
    {
        levelsButton.onClick.AddListener(Levels);
        exitGameButton.onClick.AddListener(ExitGame);
        newGameButton.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame()
    {
        GameManager.Instance.ResetAllData();
        PlayerPrefs.SetInt("UnlockedLevels", 1); //resetowanie ustawieñ do pocz¹tkowych czyli znowu levele zablokowane
        PlayerPrefs.Save();
        SceneManager.LoadScene("LevelMenu");
    }

    private void Levels()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
