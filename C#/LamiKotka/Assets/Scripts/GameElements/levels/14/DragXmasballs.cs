using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragXmasballs : MonoBehaviour
{
    public bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    [SerializeField] private GameObject tree;

    private Vector3 initialPosition; 
    public bool isOnTree = false; 

    private void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree"))
        {
            StartCoroutine(DisableDraggingWithDelay()); 
        }
    }

    private void Update()
    {
        if (isOnTree) return; 

        Vector3 inputPosition;

        // Obs³uga dotyku
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
            HandleDragging(inputPosition, touch.phase);
        }
        // Obs³uga myszy
        else if (Input.GetMouseButton(0)) 
        {
            inputPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            HandleDragging(inputPosition, Input.GetMouseButtonDown(0) ? TouchPhase.Began : TouchPhase.Moved);
        }

        
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            isDragging = false;
            if (!isOnTree)
            {
                transform.position = initialPosition; 
            }
        }
    }

    private void HandleDragging(Vector3 position, TouchPhase phase)
    {
        if (phase == TouchPhase.Began || phase == TouchPhase.Moved)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Xmasball"));

            if (hit.collider != null && hit.transform == transform)
            {
                if (phase == TouchPhase.Began)
                {
                    isDragging = true;
                    offset = transform.position - position;
                }

                if (isDragging)
                {
                    Vector3 newPosition = position + offset;
                    transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
                }
            }
        }

        if (phase == TouchPhase.Ended || phase == TouchPhase.Canceled)
        {
            isDragging = false;
        }
    }

    private IEnumerator DisableDraggingWithDelay()
    {
        yield return new WaitForSeconds(0.2f); 
        isOnTree = true; 
    }

    public void isntOnTree() 
    { 
        isOnTree=false;
    }
}
