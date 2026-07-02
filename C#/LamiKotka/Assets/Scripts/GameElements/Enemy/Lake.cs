using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;


public class Lake : MonoBehaviour
{
    public level8 level8;
    public Crate8 crate8;
   public Planks8 planks8;
    private int crateLayer=8;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent(out Player player))
        {

            player.TakeDamage("Lake");
            level8.ResetPlayerPosition();
            if (!planks8.IsFirstPlankPlaced())
            {
                level8.ResetSetings();
            } 
        }
    }
}

