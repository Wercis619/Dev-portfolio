using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    
    [SerializeField] private Button levelsButton;

    [SerializeField] private Button backToMenuButton;


    private void Start()
    {
        
        levelsButton.onClick.AddListener(Levels);

        backToMenuButton.onClick.AddListener(BackToMenu);
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
