using System.Collections;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class FireController : MonoBehaviour
{
    public GameObject fireOn; 
    public GameObject fireOff; 
    public Level7 level7; 
    public GameObject player; 
    public float blowThreshold = 0.3f; 
    private AudioClip microphoneInput;
    private bool isFireBlowable = false;

    private void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(null, true, 10, 44100);
        }
        else
        {
            Debug.LogWarning("Mikrofon niedostêpny!");
            isFireBlowable = false;
        }
        fireOn.SetActive(true);
    }
   

    private void Update()
    {
        if (isFireBlowable && Microphone.IsRecording(null))
        {
            float volume = GetMicrophoneVolume();

            if (volume > blowThreshold)
            {
                BlowOutFire();
            }
        }
    }

    private float GetMicrophoneVolume()
    {
        int sampleWindow = 128;
        float[] waveData = new float[sampleWindow];
        int microphonePosition = Microphone.GetPosition(null) - sampleWindow + 1;

        if (microphonePosition < 0) return 0;

        microphoneInput.GetData(waveData, microphonePosition);

        float sum = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += waveData[i] * waveData[i];
        }

        return Mathf.Sqrt(sum / sampleWindow);
    }

    public void EnableBlowMechanic()
    {
        isFireBlowable = true;
    }

    private void BlowOutFire()
    {
        isFireBlowable = false;
        fireOn.SetActive(false);
        fireOff.SetActive(true);
    }

}

