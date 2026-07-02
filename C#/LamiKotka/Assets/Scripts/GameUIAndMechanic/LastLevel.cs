using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastLevel : MonoBehaviour
{
    [Header("Star Images")]
    [SerializeField] private GameObject firstStarGold;
    [SerializeField] private GameObject firstStarBlack;
    [SerializeField] private GameObject secondStarGold;
    [SerializeField] private GameObject secondStarBlack;
    [SerializeField] private GameObject thirdStarGold;
    [SerializeField] private GameObject thirdStarBlack;

    [Header("Buttons")]
    [SerializeField] private Button nextButton;
   

    private void Start()
    {
        int collectedMice = GameManager.Instance.GetCollectedMice();
        int totalMice = GameManager.Instance.GetTotalMice();

        UpdateStars(collectedMice, totalMice);

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(EndGameView);
        }    
    }

    private void UpdateStars(int collectedMice, int totalMice)
    {
        firstStarGold.SetActive(false);
        secondStarGold.SetActive(false);
        thirdStarGold.SetActive(false);

        firstStarBlack.SetActive(true);
        secondStarBlack.SetActive(true);
        thirdStarBlack.SetActive(true);

        if (collectedMice == 0)
        {
            GameManager.Instance.SaveStarsForLevel(GameManager.Instance.CurrentLevelIndex, 0);
            return;
        }

        float percentage = (float)collectedMice / totalMice;

        int stars = 0;
        if (percentage <= 1f / 3f)
        {
            firstStarGold.SetActive(true);
            firstStarBlack.SetActive(false);
            stars = 1;
        }
        else if (percentage <= 2f / 3f)
        {
            firstStarGold.SetActive(true);
            secondStarGold.SetActive(true);
            firstStarBlack.SetActive(false);
            secondStarBlack.SetActive(false);
            stars = 2;
        }
        else
        {
            firstStarGold.SetActive(true);
            secondStarGold.SetActive(true);
            thirdStarGold.SetActive(true);
            firstStarBlack.SetActive(false);
            secondStarBlack.SetActive(false);
            thirdStarBlack.SetActive(false);
            stars = 3;
        }

        GameManager.Instance.SaveStarsForLevel(GameManager.Instance.CurrentLevelIndex, stars);
    }

    private void EndGameView()
    {
        SceneManager.LoadScene(20);
    }
}
