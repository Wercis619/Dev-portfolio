using UnityEngine;

public class GiftClicked : MonoBehaviour
{
    [SerializeField] private GameObject gift;
    [SerializeField] private GameObject goldfish;

    private void Update()
    {
        // Obs³uga dotyku
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
            Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

            if (hitCollider != null && hitCollider.gameObject == gift && touch.phase == TouchPhase.Began)
            {
                HandleGiftClick();
            }
        }
        // Obs³uga klikniêcia myszk¹
        else if (Input.GetMouseButtonDown(0)) // Lewy przycisk myszy
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gift)
            {
                HandleGiftClick();
            }
        }
    }

    private void HandleGiftClick()
    {
        gift.SetActive(false);
        goldfish.SetActive(true);
    }
}
