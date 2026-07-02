using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Transform graphicsTransform;
    [SerializeField] private Rigidbody2D playerRigidbody;

    [Space(5)]

    [Header("Parameters")]
    [SerializeField] private string isMovingParameter;
    [SerializeField] private string isJumpingParameter;
    [SerializeField] private string isFallingParameter;
    [SerializeField] private string hitParameter = "Hit";

    public void PlayHitAnimation()
    {
        animator.SetTrigger(hitParameter); 
    }

    private void Update()
    {
        UpdateAnimations();
        UpdateRotation();
    }

    private void UpdateAnimations()
    {
        animator.SetBool(isMovingParameter, movement.IsMoving());

        if (playerRigidbody.velocity.y > 0.1f)
        {
            animator.SetBool(isJumpingParameter, true);
            animator.SetBool(isFallingParameter, false);
        }
        else if (playerRigidbody.velocity.y < -0.1f)
        {
            animator.SetBool(isJumpingParameter, false);
            animator.SetBool(isFallingParameter, true);
        }
        else
        {
            animator.SetBool(isJumpingParameter, false);
            animator.SetBool(isFallingParameter, false);
        }
    }

    private void UpdateRotation()
    {
        if (movement.GetCurrentInputX() > 0)
        {
            graphicsTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement.GetCurrentInputX() < 0)
        {
            graphicsTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
