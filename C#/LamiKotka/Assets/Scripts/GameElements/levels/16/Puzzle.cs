using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private AudioSource puzzlePickUpSound;
    [SerializeField] private GameObject puzzleEffectPrefab;
    [SerializeField] private float puzzleEffectPrefabLifetime = 0.583f;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerPuzzle playerPuzzle))
        {
            playerPuzzle.AddPuzzle();
            Debug.Log("DŸwiêk zbierania rybki odtwarzany"); // Log do debugowania

            if (FindObjectOfType<SoundManager>().AreSoundsOn)
            {
                AudioSource.PlayClipAtPoint(puzzlePickUpSound.clip, transform.position);
            }

            var spawnedPrefab = Instantiate(puzzleEffectPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(spawnedPrefab, puzzleEffectPrefabLifetime);
            Destroy(gameObject);
        }
    }
}
