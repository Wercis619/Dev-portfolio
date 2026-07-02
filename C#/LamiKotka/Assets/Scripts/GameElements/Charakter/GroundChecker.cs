/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour { 

    [SerializeField] private string groundTag;

    private int groundCounter = 0;

    public bool isGrounded
     {
        get
            {
                return groundCounter > 0;
            }
     }

     public event Action OnLanding;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(groundTag))
        {
          if (groundCounter==0) 
          {
              OnLanding?.Invoke();
          }
         ++groundCounter;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
     {
         --groundCounter;    
     }
}
*/
using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private string groundTag; // Tag, który bêdzie u¿ywany do identyfikacji gruntu

    private int groundCounter = 0; // Licznik kontaktów z gruntem

    public bool isGrounded
    {
        get
        {
            return groundCounter > 0; // SprawdŸ, czy gracz jest na ziemi
        }
    }

    public event Action OnLanding; // Zdarzenie wywo³ywane podczas l¹dowania

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(groundTag))
        {
            if (groundCounter == 0) // Jeœli to pierwszy kontakt
            {
                OnLanding?.Invoke(); // Wywo³aj zdarzenie l¹dowania
            }
            ++groundCounter; // Zwiêksz licznik
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(groundTag))
        {
            --groundCounter; // Zmniejsz licznik przy opuszczeniu gruntu
        }
    }
}
