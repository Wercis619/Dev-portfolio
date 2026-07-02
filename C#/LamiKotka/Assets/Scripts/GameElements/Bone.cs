using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Level9 level = FindObjectOfType<Level9>(); // Pobieramy skrypt Level9
            level.PickUpBone(); // Przekazujemy informacjê, ¿e gracz ma koœæ
            gameObject.SetActive(false); // Koœæ znika
        }
    }
}