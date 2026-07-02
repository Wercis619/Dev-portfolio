using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Azor : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject boneUI; 
    [SerializeField] private AudioSource angrySound; 
    [SerializeField] private float[] distanceThresholds = { 4f, 8f, 12f, 16f, 20f };
    [SerializeField] private float[] volumeLevels = { 0.8f, 0.6f, 0.4f, 0.2f, 0f };
    private bool hasBone = false;
    private bool isHappy = false;

    private void Start()
    {
        animator.Play("AngryDog");

        if (FindObjectOfType<SoundManager>().AreSoundsOn)
        {
            angrySound.loop = true; 
          
            angrySound.Play();
        }
    }

    private void Update()
    {
        AdjustSoundVolume();
    }

    private void AdjustSoundVolume()
    {
        if (angrySound.isPlaying)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            float newVolume = volumeLevels[0]; 

            for (int i = 0; i < distanceThresholds.Length; i++)
            {
                if (distance >= distanceThresholds[i])
                {
                    newVolume = volumeLevels[i];
                }
                else
                {
                    break; 
                }
            }
            angrySound.volume = newVolume;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Level9 level = FindObjectOfType<Level9>();
            if (level != null)
            {
                hasBone = level.hasBone;
            }
            else
            {
                Debug.LogWarning("Brak obiektu Level9!");
            }

            if (!hasBone)
            {
                player.TakeDamage("Azor");
            }
            else if (!isHappy)
            {
                boneUI.SetActive(false);
                StartCoroutine(GiveBoneToAzor());
            }
        }
    }

    private IEnumerator GiveBoneToAzor()
    {
        animator.SetTrigger("GiveBone"); 
        angrySound.Stop(); 
        yield return new WaitForSeconds(1.5f); 
        animator.Play("HappyDog"); 
        isHappy = true; 
    }
}
