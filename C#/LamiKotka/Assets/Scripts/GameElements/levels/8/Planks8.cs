using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planks8 : MonoBehaviour
{
    [SerializeField] private List<GameObject> allPlanks; 
    [SerializeField] private List<Transform> targetPositions;
    [SerializeField] private GameObject platform1;
    [SerializeField] private GameObject platform2;

    private GameObject currentPlank = null; 
    private int currentPlankIndex = 0;
    private bool isDragEnabled = false; 
    private Vector3 offset;
    
    private float minX = -7f;
    private float maxX = 24f;
    private float minY = -1.6f;
    private float maxY = 3f;

    private List<Vector3> initialPositions; 

    private void Start()
    {
        platform1.SetActive(false);
        platform2.SetActive(false);
        initialPositions = new List<Vector3>();
        foreach (GameObject plank in allPlanks)
        {
            initialPositions.Add(plank.transform.position); 
            plank.SetActive(false); 
        }
    }

    public void EnablePlanksMechanic()
    {
        platform1.SetActive(true);
        for (int i = 0; i < Mathf.Min(2, allPlanks.Count); i++)
        {
            allPlanks[i].SetActive(true);
        }

        if (allPlanks.Count > 0)
        {
            currentPlank = allPlanks[0];
            SetColliderAsTrigger(currentPlank, true);

            isDragEnabled = true;
        }
    }

    private void Update()
    {
        if (isDragEnabled && currentPlank != null)
        {
            HandleDrag();
        }
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector3 mousePosition = GetInputPosition();
            if (currentPlank.GetComponent<Collider2D>().bounds.Contains(mousePosition))
            {
                offset = currentPlank.transform.position - mousePosition;
                // Zablokuj kolizje podczas przeci¹gania
                if(currentPlankIndex == 0)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Plank1"), true);
                }
                else if (currentPlankIndex == 1)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Plank2"), true);
                }
                else if (currentPlankIndex == 2)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Plank3"), true);
                }
                else if (currentPlankIndex == 3)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Plank4"), true);
                }
            }
        }
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            Vector3 mousePosition = GetInputPosition();
            Vector3 newPosition = mousePosition + offset;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            currentPlank.transform.position = newPosition;
        }
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            if (currentPlankIndex == 0)
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Plank1"), false);
            }
            else if (currentPlankIndex == 1)
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Plank2"), false);
            }
            else if (currentPlankIndex == 2)
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Plank3"), false);
            }
            else if (currentPlankIndex == 3)
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Plank4"), false);
            }
            CheckPlankPosition();
        }
    }

    private Vector3 GetInputPosition()
    {
        Vector3 inputPosition;

        if (Input.touchCount > 0)
        {
            inputPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else
        {
            inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        inputPosition.z = 0; 
        return inputPosition;
    }

    private void CheckPlankPosition()
    {
        float distance = Vector3.Distance(currentPlank.transform.position, targetPositions[currentPlankIndex].position);

        if (distance < 0.5f) 
        {
            SnapToTarget();
        }
        else
        {
            ResetPlankPosition();
        }
    }

    private void SnapToTarget()
    {
        currentPlank.transform.position = targetPositions[currentPlankIndex].position;

        SetColliderAsTrigger(currentPlank, false);

        currentPlank = null;
        currentPlankIndex++;

        if (currentPlankIndex % 2 == 0 && currentPlankIndex < allPlanks.Count)
        {
            platform2.SetActive(true);
            for (int i = currentPlankIndex; i < Mathf.Min(currentPlankIndex + 2, allPlanks.Count); i++)
            {
                allPlanks[i].SetActive(true);
            }
        }

        if (currentPlankIndex < allPlanks.Count)
        {
            currentPlank = allPlanks[currentPlankIndex];
            SetColliderAsTrigger(currentPlank, true);

        }
        else
        {
            Debug.Log("Wszystkie deski s¹ na swoich miejscach!");
        }
    }

    private void ResetPlankPosition()
    {

        currentPlank.transform.position = initialPositions[currentPlankIndex];

        SetColliderAsTrigger(currentPlank, true);


    }

    public void DisablePlanksMechanic()
    {
        foreach (GameObject plank in allPlanks)
        {
            plank.SetActive(false); 
        }
        isDragEnabled = false;
        currentPlank = null;
        currentPlankIndex = 0; 
    }

    public bool IsFirstPlankPlaced()
    {
        return currentPlankIndex > 0; 
    }

    private void SetColliderAsTrigger(GameObject plank, bool isTrigger)
    {
        Collider2D collider = plank.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = isTrigger; 
        }
    }
}