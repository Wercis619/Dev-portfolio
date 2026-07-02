using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cratelevel6 : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private level6 levelManager; 

    private float accelerationMultiplier = 100f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (levelManager == null)
        {
            levelManager = FindObjectOfType<level6>();
        }

        if (levelManager == null)
        {
            Debug.LogError("Nie znaleziono skryptu level6! Upewnij siê, ¿e jest przypisany w inspektorze.");
        }
    }

    void Update()
    {
        Vector2 tilt = Input.acceleration;

        Vector2 force = new Vector2(tilt.x, 0) * accelerationMultiplier;
        rb.AddForce(force);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GreenBox") || other.CompareTag("RedBox"))
        {
            if (levelManager != null)
            {
                levelManager.OnCrateInBox(gameObject, other.gameObject);
            }
        }
    }
}
