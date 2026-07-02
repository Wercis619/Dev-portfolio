using UnityEngine;
public class TouchDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool canDrag = false;
    public GameObject dialogUI;

    void OnMouseDown()

    {
        // Sprawdü, czy dialog jest aktywny
        if (dialogUI.activeSelf)
        {
            canDrag = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void OnMouseDrag()
    {
        if (canDrag)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            // Ogranicz ruch w osi Y do zakresu 0 do -7
            newPosition.y = Mathf.Clamp(newPosition.y, 1.5f, 7.0f);
            // Ogranicz ruch w osi X do zakresu 0.2 do 27.0
            newPosition.x = Mathf.Clamp(newPosition.x, 13.5f, 32.0f);

            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        canDrag = false;
    }
}
