using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{


    [SerializeField] private AudioSource mousePickUpSound; // U¿yj AudioSource do odtwarzania dŸwiêku
    [SerializeField] private GameObject mouseEffectPrefab;
    [SerializeField] private float mouseEffectPrefabLifetime = 0.583f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInventory playerInventory))
        {
            playerInventory.AddMouse();
            Debug.Log("DŸwiêk zbierania myszy odtwarzany");

            if (FindObjectOfType<SoundManager>().AreSoundsOn)
            {
                AudioSource.PlayClipAtPoint(mousePickUpSound.clip, transform.position);
            }

            var spawnedPrefab = Instantiate(mouseEffectPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(spawnedPrefab, mouseEffectPrefabLifetime);

            Destroy(gameObject);
        }
    }
}
