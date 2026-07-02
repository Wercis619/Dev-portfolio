
using UnityEngine;
using Cinemachine;
using System.Collections;

public class CinemachineCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Referencja do wirtualnej kamery
    public float defaultOrthographicSize = 5f; // Domyœlny rozmiar obiektywu
    public float puzzleOrthographicSize = 10f; // Rozmiar obiektywu podczas zagadki
    public float transitionSpeed = 2f; // Prêdkoœæ przejœcia miêdzy rozmiarami
    
    private bool isPuzzleActive = false; // Flaga aktywnoœci zagadki
   
    private void Start()
    {
        // Ustaw domyœlny rozmiar obiektywu
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.OrthographicSize = defaultOrthographicSize;
        }
    }

    public void ActivatePuzzleCamera()
    {
        isPuzzleActive = true; // Ustaw flagê aktywnoœci zagadki
        if (virtualCamera != null)
        {
            StartCoroutine(ChangeCameraSize(puzzleOrthographicSize));
        }
    }

    public void DeactivatePuzzleCamera()
    {
        isPuzzleActive = false; // Ustaw flagê aktywnoœci zagadki
        if (virtualCamera != null)
        {
            StartCoroutine(ChangeCameraSize(defaultOrthographicSize));
        }
    }

    private IEnumerator ChangeCameraSize(float targetSize)
    {
        float currentSize = virtualCamera.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < transitionSpeed)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentSize, targetSize, elapsedTime / transitionSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize; // Ustaw koñcowy rozmiar
    }
}

