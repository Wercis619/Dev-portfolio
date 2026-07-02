using UnityEngine;
using UnityEngine.UI;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject flashOnObject; 
    public GameObject flashOffObject; 
    public Image image1; 
    public Image image2; 
    public GameObject canvas1; 

    private bool isFlashlightOn = false; 

    private void Start()
    {
        isFlashlightOn = false; 
        UpdateUI();
    }

    public void ToggleFlashlight()
    {
        // Zmieñ stan latarki
        isFlashlightOn = !isFlashlightOn;

        // Zaktualizuj UI
        UpdateUI();
    }

    private void UpdateUI()
    {
        flashOnObject.SetActive(!isFlashlightOn); 
        flashOffObject.SetActive(isFlashlightOn); 
        image1.gameObject.SetActive(!isFlashlightOn); 
        image2.gameObject.SetActive(isFlashlightOn); 
        canvas1.SetActive(!isFlashlightOn); 
    }
}