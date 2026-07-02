using UnityEngine;

public class DragSnowball : MonoBehaviour
{
    [SerializeField] Level15 level15;
    private bool isDragging = false;
    private bool isDraggable = false;
    private bool hasBeenPlaced = false;
   
    private Camera mainCamera;
    [SerializeField] private Transform fishTransform;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isDragging && isDraggable)
        {
            DragSnow();
        }
    }

    private void OnMouseDown()
    {
        if (!hasBeenPlaced)
        {
            isDragging = true;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Snow"), LayerMask.NameToLayer("Player"), true);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Snow"), LayerMask.NameToLayer("Player"), false);

            if (fishTransform != null && Mathf.Abs(transform.position.x - fishTransform.position.x) >= 1.8f)
            {
                hasBeenPlaced = true;
                level15.snowballPlaced = true;
                DisableDragging();
            }
        }
    }

    private void DragSnow()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        float clampedX = Mathf.Clamp(worldPosition.x, -45.8f, -41.0f);
        float clampedY = Mathf.Clamp(worldPosition.y, -9.0f, -3.5f);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void EnableDragging()
    {
        isDraggable = true;
    }

    public void DisableDragging()
    {
        isDraggable = false;
    }
}
