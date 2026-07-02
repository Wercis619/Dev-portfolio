using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LostUI : MonoBehaviour
{
    [SerializeField] private Button restartLevelButton;
    [SerializeField] private Button levelsButton;
   
    [SerializeField] private Button backToMenuButton;
    

    private void Start()
    {
        restartLevelButton.onClick.AddListener(RestartLevel);
        levelsButton.onClick.AddListener(Levels);
        
        backToMenuButton.onClick.AddListener(BackToMenu);
    }

    private void RestartLevel()
    {
        GameManager.Instance.RestartLevel();

    }

    private void Levels()
    {
        SceneManager.LoadScene(1);

    }

    
    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
