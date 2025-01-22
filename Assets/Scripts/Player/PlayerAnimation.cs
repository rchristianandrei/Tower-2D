using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private Animator animator;

    private const string IS_WALKING = "IsWalking";
    private const string IS_JUMPING = "IsJumping";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerMovement.OnPlayerMoved += PlayerMovement_OnPlayerMoved;
        playerMovement.OnPlayerJump += PlayerMovement_OnPlayerJump;
        playerMovement.OnPlayerLand += PlayerMovement_OnPlayerLand;
    }

    private void PlayerMovement_OnPlayerLand(object sender, EventArgs e)
    {
        animator.SetBool(IS_JUMPING, false);
    }

    private void PlayerMovement_OnPlayerJump(object sender, EventArgs e)
    {
        animator.SetBool(IS_JUMPING, true);
    }

    private void PlayerMovement_OnPlayerMoved(object sender, float dir)
    {
        if (dir > 0)
        {
            transform.localScale = new(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (dir < 0)
        {
            transform.localScale = new(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        animator.SetBool(IS_WALKING, dir != 0);
    }
}
