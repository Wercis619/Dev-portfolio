/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pimpek : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private GameObject pimpekParticlePrefab;
    [SerializeField] private float pimpekParticleLifetime = 1f;
    [SerializeField] private Transform pimpekFly;

    private Animator animator; 

    private bool isDragging = false; 
    private bool isDraggable = false; 
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        SetIdleAnimation();
    }

    private void Update()
    {
        if (isDragging && isDraggable)
        {
            DragPimpek();
        }
    }

    private void OnMouseDown()
    {
        if (isDraggable && transform.position.y <= pimpekFly.position.y)
        {
            isDragging = true;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Pimpek"), LayerMask.NameToLayer("Player"), true);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            CheckIfRescued();
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Pimpek"), LayerMask.NameToLayer("Player"), true);
        }
    }

    private void DragPimpek()
    {
        SetFearAnimation();
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; 
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }

    private void CheckIfRescued()
    {
        if (transform.position.y > pimpekFly.position.y)
        {
            FindObjectOfType<Level4>().NotifyPimpekRescued();
            DisableDragging();
        }
    }

    public void EnableDragging()
    {
        isDraggable = true; 
        SetFearAnimation();
    }

    public void DisableDragging()
    {
        isDraggable = false; 
    }

    public void TakeDamage()
    {
        if (FindObjectOfType<SoundManager>().AreSoundsOn)
        {
            AudioSource.PlayClipAtPoint(damageSound.clip, transform.position);
        }
        var spawnedPrefab = Instantiate(pimpekParticlePrefab, transform.position, Quaternion.identity);
        Destroy(spawnedPrefab, pimpekParticleLifetime);
        DisableDragging();
        FindObjectOfType<Level4>().ResetLevel();

    }

    private void SetFearAnimation()
    {
        animator.SetTrigger("Fear");
    }

    public void SetIdleAnimation()
    {
        animator.ResetTrigger("Fear");
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pimpek : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private GameObject pimpekParticlePrefab;
    [SerializeField] private float pimpekParticleLifetime = 1f;
    [SerializeField] private Transform pimpekFly;
    [SerializeField] private Collider2D legsCollider; // Collider2D przypisany do nóg Pimka

    private Animator animator;

    private bool isDragging = false;
    private bool isDraggable = false;
    private bool isRescued = false; // Flaga, która sprawdzi, czy Pimpek zosta³ uratowany
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        SetIdleAnimation();
    }

    private void Update()
    {
        if (isDragging && isDraggable)
        {
            DragPimpek();
        }
    }

    private void OnMouseDown()
    {
        if (isDraggable && legsCollider.transform.position.y <= pimpekFly.position.y)
        {
            isDragging = true;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Pimpek"), LayerMask.NameToLayer("Player"), true);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            CheckIfRescued();
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Pimpek"), LayerMask.NameToLayer("Player"), true);
        }
    }

    private void DragPimpek()
    {
        SetFearAnimation();
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Ograniczanie pozycji Pimka na osi X i Y
        float clampedX = Mathf.Clamp(worldPosition.x, 13.0f, 22.5f);
        float clampedY = Mathf.Clamp(worldPosition.y, -4.5f, 6.5f);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void CheckIfRescued()
    {
        if (legsCollider.transform.position.y > pimpekFly.position.y)
        {
            isRescued = true; // Ustawienie flagi uratowania na true
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawdzanie, czy to nogi Pimka wesz³y w kolizjê z obiektem o tagu "Ground"
        if (legsCollider.IsTouching(collision) && collision.CompareTag("Ground") && isRescued)
        {
            // Uruchomienie dialogu i aktywacji rybki dopiero po kontakcie nóg Pimka z ziemi¹
            FindObjectOfType<Level4>().NotifyPimpekRescued();
            DisableDragging();
        }
    }

    public void EnableDragging()
    {
        isDraggable = true;
        SetFearAnimation();
    }

    public void DisableDragging()
    {
        isDraggable = false;
    }

    public void TakeDamage()
    {
        if (FindObjectOfType<SoundManager>().AreSoundsOn)
        {
            AudioSource.PlayClipAtPoint(damageSound.clip, transform.position);
        }
        var spawnedPrefab = Instantiate(pimpekParticlePrefab, transform.position, Quaternion.identity);
        Destroy(spawnedPrefab, pimpekParticleLifetime);

        DisableDragging();

        // Reset rescue status and level
        isRescued = false;
        FindObjectOfType<Level4>().ResetLevel();
    }

    private void SetFearAnimation()
    {
        animator.SetTrigger("Fear");
    }

    public void SetIdleAnimation()
    {
        animator.ResetTrigger("Fear");
    }
}


