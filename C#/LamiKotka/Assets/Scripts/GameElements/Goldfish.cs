using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldfish : MonoBehaviour
{
  
    
    [SerializeField] private AudioSource fishPickUpSound;
    [SerializeField] private GameObject fishEffectPrefab;
    [SerializeField] private float fishEffectPrefabLifetime = 0.583f;


   public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInventory playerInventory))
        {
            playerInventory.AddFish();
            Debug.Log("DŸwiêk zbierania rybki odtwarzany"); // Log do debugowania

            if (FindObjectOfType<SoundManager>().AreSoundsOn)
            {
                AudioSource.PlayClipAtPoint(fishPickUpSound.clip, transform.position);
            }

            var spawnedPrefab = Instantiate(fishEffectPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(spawnedPrefab, fishEffectPrefabLifetime);
            Destroy(gameObject);
        }
    }
}

