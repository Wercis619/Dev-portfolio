using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate8 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D crateRigidbody;
    [SerializeField] private Transform crate;
    [SerializeField] private Transform resetCratePosition; 
    [SerializeField] private float shakeThreshold = 2.0f;

    private bool isThrowEnabled = false;
    private bool isCrateActivated = false;

    void Start()
    {
        crateRigidbody.isKinematic = true; 
    }

    void Update()
    {
        if (!isCrateActivated && IsShaking() && isThrowEnabled)
        {
            ActivateCrate();
        }
    }

    bool IsShaking()
    {
        Vector3 acceleration = Input.acceleration;
        return acceleration.magnitude > shakeThreshold;
    }

    void ActivateCrate()
    {
        isCrateActivated = true;
        crateRigidbody.isKinematic = false; 
    }

    public void DisactivateCrate()
    {
        isCrateActivated = false;
        crateRigidbody.isKinematic = true; 
    }

    public void EnableThrowMechanic()
    {
        isThrowEnabled = true;
    }

    public void DisableThrowMechanic()
    {
        isThrowEnabled = false;
    }

    public void ResetCratePosition()
    {
        DisactivateCrate();
        DisableThrowMechanic();
        crate.transform.position = resetCratePosition.position; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lake"))
        {
            ResetCratePosition();
        }
    }
}
