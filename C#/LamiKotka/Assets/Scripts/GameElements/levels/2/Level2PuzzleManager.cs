using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Level2PuzzleManager : MonoBehaviour
{
    [Header("References")]
    public GameObject player; // Referencja do gracza
    public GameObject puzzleSign; // Znak aktywuj¹cy zagadkê
    public GameObject blockingPlank; // Blokada do rybki
    public GameObject dialogUI; // UI dialogu

    [Header("Crates")]
    public List<GameObject> allCrates; // Lista wszystkich skrzyñ
    public Transform platformTransform; // Transform platformy (gdzie uk³ada siê skrzynie)

    // Skrzynie do wygranej
    public GameObject GreenCrate;
    public GameObject YellowCrate;
    public GameObject RedCrate;

    [Header("Puzzle Settings")]
    public int maxCratesOnPlatform = 3; 
    public float resetDelay = 2f; 
    public float displayTime = 3f; 

    private List<GameObject> cratesOnPlatform = new List<GameObject>(); 
    private Vector3[] initialCratePositions; 
    private bool isPuzzleActive = false; 

    private void Start()
    {
        initialCratePositions = new Vector3[allCrates.Count];
        for (int i = 0; i < allCrates.Count; i++)
        {
            initialCratePositions[i] = allCrates[i].transform.position;
            allCrates[i].SetActive(false); 
            DeactivateText(allCrates[i]); 
        }
        FindObjectOfType<CinemachineCameraController>().DeactivatePuzzleCamera();

        dialogUI.SetActive(false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Crate"), false);
    }

    private void Update()
    {
        if (!isPuzzleActive && Vector3.Distance(player.transform.position, puzzleSign.transform.position) < 1.5f)
        {
            StartPuzzle();
        }
    }

    private void StartPuzzle()
    {
        isPuzzleActive = true;
        dialogUI.SetActive(true);
        FreezePlayer(); 
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Crate"), true); 
        ActivateCrates();
        Debug.Log("Puzzle started. Player movement frozen.");
        FindObjectOfType<CinemachineCameraController>().ActivatePuzzleCamera();
    }

    private void ActivateCrates()
    {
        foreach (GameObject crate in allCrates)
        {
            crate.SetActive(true);
            DeactivateText(crate); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (isPuzzleActive)
        {
            GameObject crate = collision.gameObject;

            if (allCrates.Contains(crate) && !cratesOnPlatform.Contains(crate))
            {
                cratesOnPlatform.Add(crate);
                Debug.Log("Crate added: " + crate.name);

                if (cratesOnPlatform.Count > maxCratesOnPlatform)
                {
                    Debug.Log("Too many crates on platform. Displaying messages...");
                    StartCoroutine(DisplayMessagesAndReset());
                }
                else if (cratesOnPlatform.Count == 3)
                {
                    if (!cratesOnPlatform.Contains(GreenCrate) ||
                        !cratesOnPlatform.Contains(YellowCrate) ||
                        !cratesOnPlatform.Contains(RedCrate))
                    {
                        Debug.Log("One or more required crates are missing. Resetting...");
                        StartCoroutine(DisplayMessagesAndReset());
                    }
                    else
                    {
                        Debug.Log("All required crates are on the platform! Puzzle solved.");
                        OnPuzzleSolved();
                    }
                }
                else
                {
                    Debug.Log("Current number of crates on platform: " + cratesOnPlatform.Count);
                }
            }
        }
    }

    private IEnumerator DisplayMessagesAndReset()
    {
        foreach (GameObject crate in cratesOnPlatform)
        {
            ActivateText(crate);
            Debug.Log("Displaying message on crate: " + crate.name);
        }

        yield return new WaitForSeconds(displayTime);
        yield return StartCoroutine(ResetPuzzle());
    }

    private void DisplayMessages()
    {
        foreach (GameObject crate in cratesOnPlatform)
        {
            ActivateText(crate);
            Debug.Log("Displaying message on crate: " + crate.name);
        }   
        
    }

    private IEnumerator ResetPuzzle()
    {
        yield return new WaitForSeconds(resetDelay);

        foreach (GameObject crate in cratesOnPlatform)
        {
            crate.transform.position = initialCratePositions[allCrates.IndexOf(crate)];
            DeactivateText(crate); 
        }

        cratesOnPlatform.Clear(); 
        dialogUI.SetActive(true); 

        Debug.Log("Puzzle reset. Crates returned to initial positions.");
    }

    private void OnPuzzleSolved()
    {

        FindObjectOfType<CinemachineCameraController>().DeactivatePuzzleCamera();
        dialogUI.SetActive(false);
        blockingPlank.SetActive(false);
        DisplayMessages();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Crate"), false);
       
        foreach (GameObject crate in cratesOnPlatform)
        {
            if (crate == GreenCrate || crate == YellowCrate || crate == RedCrate)
            {
                crate.SetActive(true); 
            }
        }

        foreach (GameObject crate in allCrates)
        {
            if (crate != GreenCrate && crate != YellowCrate && crate != RedCrate)
            {
                crate.SetActive(false); 
            }
        }
       
        UnfreezePlayer(); // Odblokuj ruch gracza
        Debug.Log("Player movement unblocked.");
    }

    private void FreezePlayer()
    {
        player.GetComponent<PlayerMovement>().FreezeMovement();
        Debug.Log("Player movement frozen.");
    }

    private void UnfreezePlayer()
    {
        player.GetComponent<PlayerMovement>().UnfreezeMovement();
        Debug.Log("Player movement unblocked.");
    }

    private TMP_Text FindTextMeshPro(GameObject crate)
    {
        var textMeshPro = crate.GetComponentInChildren<TMP_Text>();
        if (textMeshPro == null)
        {
            Debug.LogWarning($"TMP_Text not found in crate {crate.name}");
        }
        return textMeshPro;
    }

    private void ActivateText(GameObject crate)
    {
        var textMeshPro = FindTextMeshPro(crate);
        if (textMeshPro != null)
        {
            textMeshPro.enabled = true;
        }
    }

    private void DeactivateText(GameObject crate)
    {
        var textMeshPro = FindTextMeshPro(crate);
        if (textMeshPro != null)
        {
            textMeshPro.enabled = false;
        }
    }
} 




























