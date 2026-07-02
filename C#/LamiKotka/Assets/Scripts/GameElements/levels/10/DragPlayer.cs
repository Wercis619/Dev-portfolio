using UnityEngine;

public class DragPlayer : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    private float minX = 18f, maxX = 22f;
    private float minY = -3.5f, maxY = 3.5f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Player"));

                if (hit.collider != null && hit.transform == transform && IsWithinBounds(transform.position.x))
                {
                    isDragging = true;
                    offset = transform.position - touchPosition;
                }
            }

            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector3 newPosition = touchPosition + offset;

                if (!IsWithinBounds(newPosition.x))
                {
                    isDragging = false;
                    return;
                }

                float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
                float clampedY = Mathf.Clamp(newPosition.y, minY, maxY);
                transform.position = new Vector3(clampedX, clampedY, transform.position.z);
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
    }

    private bool IsWithinBounds(float xPosition)
    {
        return xPosition >= minX && xPosition <= maxX;
    }
}
