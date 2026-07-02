using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartLevelButton;
    [SerializeField] private Button levelsButton;

    private GameUI gameUI;

    [Header("Music & Sound Buttons")]
    [SerializeField] private GameObject musicOffButton; // Przycisk "Wy³¹cz muzykê"
    [SerializeField] private GameObject musicOnButton; // Przycisk "W³¹cz muzykê"
    [SerializeField] private GameObject soundOffButton; // Przycisk "Wy³¹cz dŸwiêk"
    [SerializeField] private GameObject soundOnButton; // Przycisk "W³¹cz dŸwiêk"

    [SerializeField] private SoundManager soundManager;

    private void Start()
    {
        // Przypisanie akcji do przycisków
        if (musicOffButton != null)
            musicOffButton.GetComponent<Button>().onClick.AddListener(() => ToggleMusic(false));

        if (musicOnButton != null)
            musicOnButton.GetComponent<Button>().onClick.AddListener(() => ToggleMusic(true));

        if (soundOffButton != null)
            soundOffButton.GetComponent<Button>().onClick.AddListener(() => ToggleSound(false));

        if (soundOnButton != null)
            soundOnButton.GetComponent<Button>().onClick.AddListener(() => ToggleSound(true));

        UpdateMusicButtons();
        UpdateSoundButtons();
    }

    private void ToggleMusic(bool isOn)
    {
        if (soundManager != null)
        {
            soundManager.ToggleMusic();
            UpdateMusicButtons();
        }
    }

    private void ToggleSound(bool isOn)
    {
        if (soundManager != null)
        {
            soundManager.ToggleSounds(); // Zmieñ stan dŸwiêków
            UpdateSoundButtons(); // Zaktualizuj widocznoœæ przycisków
        }
    }

    private void UpdateMusicButtons()
    {
        // Sprawdzenie stanu muzyki
        bool musicOn = soundManager != null && soundManager.IsMusicOn;
        musicOffButton.SetActive(musicOn);
        musicOnButton.SetActive(!musicOn);
    }

    private void UpdateSoundButtons()
    {
        soundOffButton.SetActive(soundManager.AreSoundsOn); // Widoczny, gdy dŸwiêki s¹ w³¹czone
        soundOnButton.SetActive(!soundManager.AreSoundsOn); // Widoczny, gdy dŸwiêki s¹ wy³¹czone
    }

    private void Awake()
    {
        // Sprawdzanie przypisania elementów UI
        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu nie zosta³o przypisane w Inspectorze!");
        }
        else
        {
            pauseMenu.SetActive(false);
        }

        gameUI = FindObjectOfType<GameUI>();

        // Sprawdzanie przypisania przycisków
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }
        else
        {
            Debug.LogError("ResumeButton nie zosta³ przypisany w Inspectorze!");
        }

        if (restartLevelButton != null)
        {
            restartLevelButton.onClick.AddListener(RestartLevel);
        }
        else
        {
            Debug.LogError("restartLevelButton nie zosta³ przypisany w Inspectorze!");
        }

        if (levelsButton != null)
        {
            levelsButton.onClick.AddListener(Levels);
        }
        else
        {
            Debug.LogError("levelsButton nie zosta³ przypisany w Inspectorze!");
        }
    }

  

    public void ShowPauseMenu()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu nie zosta³o przypisane w Inspectorze!");
            return;
        }

        pauseMenu.SetActive(true); // Poka¿ menu pauzy
        Debug.Log("PauseMenu zosta³o aktywowane.");
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    private void ResumeGame()
    {
        gameUI?.ResumeGame();
    }

    private void RestartLevel()
    {
        gameUI?.RestartLevel();
    }

    private void Levels()
    {
        gameUI?.Levels();
    }
}

