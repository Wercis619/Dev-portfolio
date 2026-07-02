using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;


public class level6 : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera redCrateCamera;
    [SerializeField] private CinemachineVirtualCamera greenCrateCamera;
    [SerializeField] private PlayerMovement playerMovement;

    public GameObject greenCrate, redCrate;
    public GameObject greenBox, redBox;
    public GameObject logObstacle;

    private GameObject activeCrate;

    private bool isGameCompleted = false;

    private bool isPuzzleActive = false;

    private bool isGreenCrateInGreenBox = false;
    private bool isGreenCrateInRedBox = false;
    private bool isRedCrateInRedBox = false;
    private bool isRedCrateInGreenBox = false;

    private Vector3 greenCrateStartPos, redCrateStartPos;

    void Start()
    {
        if (isGameCompleted)
        {
            Debug.Log("Gra ju¿ zosta³a ukoñczona, nie mo¿na jej ponownie uruchomiæ.");
            return;
        }
        SetPlayerCameraActive();

        greenCrateStartPos = greenCrate.transform.position;
        redCrateStartPos = redCrate.transform.position;

        redCrate.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPuzzleActive)
        {
            if (isGameCompleted)
            {
                Debug.Log("Gra ju¿ zosta³a ukoñczona, nie mo¿na jej ponownie uruchomiæ.");
                return;
            }
            else
            {
                Debug.Log("Gracz wszed³ w trigger");
                StartPuzzle();
            }
           
        }
    }

    private void StartPuzzle()
    {
        isPuzzleActive = true;

        playerMovement.FreezeMovement();

        activeCrate = greenCrate;
        SetCrateCameraActive(activeCrate);
    }

    public void OnCrateInBox(GameObject crate, GameObject box)
    {
        StartCoroutine(HandleCrateCollisionWithDelay(crate, box));
    }

    private IEnumerator HandleCrateCollisionWithDelay(GameObject crate, GameObject box)
    {
        Debug.Log($"Kolizja wykryta: {crate.name} z {box.name}, czekanie 1 sekundy...");
        yield return new WaitForSeconds(1f);

        if (crate == greenCrate)
        {
            if (box == greenBox)
            {
                Debug.Log("Zielona skrzynia wpad³a do zielonego pude³ka.");
                isGreenCrateInGreenBox = true; 
            }
            else if (box == redBox)
            {
                Debug.Log("Zielona skrzynia wpad³a do czerwonego pude³ka.");
                isGreenCrateInRedBox = true; 
            }

            redCrate.SetActive(true);
            activeCrate = redCrate;
            SetCrateCameraActive(activeCrate);
        }
        else if (crate == redCrate)
        {
            if (box == redBox)
            {
                Debug.Log("Czerwona skrzynia wpad³a do czerwonego pude³ka.");
                isRedCrateInRedBox = true; 
            }
            else if (box == greenBox)
            {
                Debug.Log("Czerwona skrzynia wpad³a do zielonego pude³ka.");
                isRedCrateInGreenBox = true; 
            }

            CheckPuzzleSolution();
        }
    }

    private void CheckPuzzleSolution()
    {
        bool areBothCratesInBoxes = (isGreenCrateInGreenBox || isGreenCrateInRedBox) &&
                                    (isRedCrateInRedBox || isRedCrateInGreenBox);

        if (!areBothCratesInBoxes)
        {
            return;
        }

        if (isGreenCrateInGreenBox && isRedCrateInRedBox)
        {
            Debug.Log("Poprawne rozwi¹zanie! Zielona w zielonym i czerwona w czerwonym.");
            EndPuzzle(true); 
        }
        else
        {
            ResetPuzzle();
        }
    }

    private void EndPuzzle(bool success)
    {
        if (success)
        {
            isPuzzleActive = false;
            logObstacle.SetActive(false);
            SetPlayerCameraActive();
            playerMovement.UnfreezeMovement();
            isGameCompleted = true;
        }
    }

    private void ResetPuzzle()
    {
        greenCrate.transform.position = greenCrateStartPos;
        redCrate.transform.position = redCrateStartPos;
        redCrate.SetActive(false);
        isGreenCrateInGreenBox = false;
        isGreenCrateInRedBox = false;
        isRedCrateInRedBox = false;
        isRedCrateInGreenBox = false;
        activeCrate = greenCrate;
        SetCrateCameraActive(activeCrate);
    }

    private void SetPlayerCameraActive()
    {
        playerCamera.gameObject.SetActive(true);
        greenCrateCamera.gameObject.SetActive(false);
        redCrateCamera.gameObject.SetActive(false);
    }

    private void SetCrateCameraActive(GameObject crate)
    {
        playerCamera.gameObject.SetActive(false);
        greenCrateCamera.gameObject.SetActive(false);
        redCrateCamera.gameObject.SetActive(false);

        if (crate == greenCrate)
        {
            greenCrateCamera.gameObject.SetActive(true);
            greenCrateCamera.Follow = greenCrate.transform;
            greenCrateCamera.LookAt = greenCrate.transform;
        }
        else if (crate == redCrate)
        {
            redCrateCamera.gameObject.SetActive(true);
            redCrateCamera.Follow = redCrate.transform;
            redCrateCamera.LookAt = redCrate.transform;
        }
    }
}
