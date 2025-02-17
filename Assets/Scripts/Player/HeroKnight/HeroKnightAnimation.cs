using System;
using UnityEngine;

public class HeroKnightAnimation : MonoBehaviour
{
    [SerializeField] private HeroKnight heroKnight;
    [SerializeField] private HeroKnightMovement playerMovement;
    [SerializeField] private HeroKnightAttack playerAttack;

    private Animator animator;

    private const string IS_WALKING = "IsWalking";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_ATTACKING = "IsAttacking";

    private Action endOfAttackCallback;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerMovement.OnPlayerMoved += PlayerMovement_OnPlayerMoved;
        playerMovement.OnPlayerJump += PlayerMovement_OnPlayerJump;
        playerMovement.OnPlayerLand += PlayerMovement_OnPlayerLand;

        playerAttack.OnPlayerPrimaryAttack += PlayerAttack_OnPlayerPrimaryAttack; ;
    }

    private void PlayerAttack_OnPlayerPrimaryAttack(object sender, HeroKnightAttack.OnPlayerPrimaryAttackEvent e)
    {
        animator.SetBool(IS_ATTACKING, true);
        endOfAttackCallback = e.afterAttackCallback;
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
        if (!heroKnight.GetCanMove()) return;

        if (dir > 0)
        {
            transform.localScale = new(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (dir < 0)
        {
            transform.localScale = new(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        animator.SetBool(IS_WALKING, dir != 0);
    }

    public void EndOfPrimaryAttack()
    {
        animator.SetBool(IS_ATTACKING, false);
        endOfAttackCallback();
    }
}
