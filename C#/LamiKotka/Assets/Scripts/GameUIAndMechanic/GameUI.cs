using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("Animal Counters")]
    [SerializeField] private TextMeshProUGUI mouseCounterText;
    [SerializeField] private TextMeshProUGUI fishCounterText;

    [Header("Lives Counter")]
    [SerializeField] private TextMeshProUGUI livesCounterText;

    [Header("Pause")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private PauseUI pauseUI; 

    private Player player;
    private PlayerInventory playerInventory;

    private int miceOnMap;
    private int goldfishOnMap;

    private void Awake()
    {
        
        if (pauseUI == null)
        {
            pauseUI = FindObjectOfType<PauseUI>();
            if (pauseUI == null)
            {
                Debug.LogError("PauseUI nie zosta³o znalezione ani przypisane! Upewnij siê, ¿e obiekt jest aktywny i przypisany.");
            }
        }

        playerInventory = FindObjectOfType<PlayerInventory>();
        player = FindObjectOfType<Player>();

        if (playerInventory == null || player == null)
        {
            Debug.LogError("Nie znaleziono Player lub PlayerInventory!");
        }

        var mice = FindObjectsOfType<Mouse>();
        GameManager.Instance.SetTotalMice(mice.Length);

        var goldfish = FindObjectsOfType<Goldfish>();
        goldfishOnMap = goldfish.Length;

        
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseGame);
        }
        else
        {
            Debug.LogError("PauseButton nie zosta³ przypisany w Inspectorze!");
        }
    }

    private void Update()
    {

        mouseCounterText.text = $"{GameManager.Instance.GetCollectedMice()}/{GameManager.Instance.GetTotalMice()}";
        fishCounterText.text = $"{playerInventory.GetCollectedGoldfish()}/{goldfishOnMap}";
        livesCounterText.text = $"{player.GetCurrentLives()}/{player.GetMaxLives()}";

        if (playerInventory.GetCollectedGoldfish()== goldfishOnMap && playerInventory.GetCollectedGoldfish()>=1)
        {
            Won();
        }
       
    }

    public void Won()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Indeks aktualnej sceny

        
        if (currentSceneIndex == 18) // Ostatni poziom
        {
            SceneManager.LoadScene(21); // Za³aduj scenê Win2
        }
        else
        {
            SceneManager.LoadScene(19); // Za³aduj domyœln¹ scenê Win
        }
    }

    public void PauseGame()
    {
        Debug.Log("PauseGame called!"); 

        if (pauseUI == null)
        {
            Debug.LogError("PauseUI jest null! Nie mo¿na wstrzymaæ gry.");
            return;
        }

        Time.timeScale = 0f; 
        pauseUI.ShowPauseMenu(); 

        Debug.Log("Pause menu powinno byæ teraz widoczne.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; 
        if (pauseUI != null)
        {
            pauseUI.HidePauseMenu(); 
        }
    }

    public void  RestartLevel()
    {
        Time.timeScale = 1f;
       
        GameManager.Instance.RestartLevel();
    }

    public void Levels()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

   
}
