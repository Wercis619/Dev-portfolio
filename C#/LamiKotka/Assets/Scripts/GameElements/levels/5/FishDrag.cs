using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDrag : MonoBehaviour
{
    private bool isDragging = false;
    private bool isDraggable = false;
    private Camera mainCamera;
    private Vector3 initialPosition;
    private Transform player;
    [SerializeField] private GameObject goldfishimage;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isDragging && isDraggable)
        {
            DragFish();
        }
    }

    private void OnMouseDown()
    {
        if (isDraggable)
        {
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            CheckIfRescued();
        }
    }

    private void DragFish()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Ustaw g³êbokoœæ
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }

    private void CheckIfRescued()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < 1f) 
            {
                player.GetComponent<PlayerInventory>().AddFish();
                Destroy(gameObject); 
            }
            else
            {
                transform.position = initialPosition;
            }
        }
    }

    public void EnableDragging()
    {
        isDraggable = true;
    }

    public void DisableDragging()
    {
        isDraggable = false;
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform; 
    }

    public void SetInitialPosition()
    {
        float offsetY = -1.7f;
        float offsetX = -2.3f;
        Vector3 screenPosition = goldfishimage.transform.position; 
        screenPosition.z = 10f; 
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition); 
        transform.position = new Vector3(worldPosition.x + offsetX, worldPosition.y + offsetY, transform.position.z);
        initialPosition = transform.position; 
    }
}







