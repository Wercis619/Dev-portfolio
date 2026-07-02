using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private AudioSource musicSource; // Rêcznie przypisane Ÿród³o dŸwiêku dla muzyki

    [Header("Sound Settings")]
    [SerializeField] private AudioSource[] soundSources;

    public bool AreSoundsOn { get; private set; } = true; // Domyœlnie dŸwiêki s¹ w³¹czone
    public bool IsMusicOn { get; private set; } = true;

    private void Awake()
    {
        // Upewnij siê, ¿e tablica nie jest pusta
        if (soundSources.Length == 0)
        {
            Debug.LogError("Brak przypisanych AudioSource w SoundManager!");
        }
    }

    public void ToggleSounds()
    {
        AreSoundsOn = !AreSoundsOn;
        foreach (var source in soundSources)
        {
            if (source != null)
            {
                source.mute = !AreSoundsOn; // Wycisz wszystkie Ÿród³a dŸwiêku
            }
            else
            {
                Debug.LogWarning("Znaleziono pusty AudioSource w soundSources!");
            }
        }
        Debug.Log("DŸwiêki w³¹czone: " + AreSoundsOn);
    }


    public void PlaySound(AudioSource source)
    {
        if (AreSoundsOn && source != null)
        {
            Debug.Log("Odtwarzanie dŸwiêku: " + source.clip.name); // Log dŸwiêku
            source.Play();
            Debug.Log("DŸwiêk odtwarzany: " + source.isPlaying); // Log sprawdzaj¹cy, czy dŸwiêk jest odtwarzany
        }
        else
        {
            Debug.LogWarning("Nie mo¿na odtworzyæ dŸwiêku: " + (AreSoundsOn ? "source jest null" : "dŸwiêki s¹ wyciszone"));
        }
    }

    private void Start()
    {
        // Ustawienia pocz¹tkowe dla muzyki
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.Play();
        }     
    }

    public void ToggleMusic()
    {
        IsMusicOn = !IsMusicOn;
        if (musicSource != null)
        {
            musicSource.mute = !IsMusicOn;
        }
    }
 
}


