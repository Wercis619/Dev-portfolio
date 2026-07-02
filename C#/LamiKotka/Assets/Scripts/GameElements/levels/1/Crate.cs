using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject dialogUI; // Dymek dialogowy
    public Transform player; // Referencja do gracza
    public float activationDistance = 7.0f; // Maksymalna odleg³oœæ, aby wyœwietliæ dymek
    public Rigidbody2D crateRigidbody; // Rigidbody2D skrzyni
    public float shakeThreshold = 2.0f; // Próg potrz¹œniêcia

    private bool isDialogShown = false; // Czy dymek zosta³ wyœwietlony
    private bool isCrateActivated = false; // Czy skrzynia zosta³a ju¿ aktywowana

    void Start()
    {
        // Ukryj dymek na starcie i ustaw skrzyniê jako statyczn¹
        dialogUI.SetActive(false);
        crateRigidbody.isKinematic = true;
    }

    void Update()
    {
        // SprawdŸ odleg³oœæ miêdzy graczem a skrzyni¹
        float distance = Vector3.Distance(transform.position, player.position);

        // Jeœli gracz jest wystarczaj¹co blisko i dymek jeszcze siê nie pojawi³
        if (!isDialogShown && distance <= activationDistance)
        {
            ShowDialog();
        }

        // Sprawdzaj potrz¹sniêcie tylko wtedy, gdy dymek jest widoczny
        if (isDialogShown && !isCrateActivated && IsShaking())
        {
            ActivateCrate();
        }
    }

    void ShowDialog()
    {
        isDialogShown = true;
        dialogUI.SetActive(true); // Wyœwietl dymek
    }

    bool IsShaking()
    {
        // SprawdŸ przyspieszenie urz¹dzenia
        Vector3 acceleration = Input.acceleration;
        return acceleration.magnitude > shakeThreshold;
    }

    void ActivateCrate()
    {
        isCrateActivated = true;

        // Aktywuj fizykê skrzyni
        crateRigidbody.isKinematic = false;

        // Ukryj dymek
        dialogUI.SetActive(false);
    }

}
