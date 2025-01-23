using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public event EventHandler<OnPlayerPrimaryAttackEvent> OnPlayerPrimaryAttack;
    public class OnPlayerPrimaryAttackEvent : EventArgs
    {
        public Action afterAttackCallback;
    }

    private PlayerMovement PlayerMovement;

    [SerializeField] private float attackCooldownMax = 0.75f;
    [SerializeField] private BoxCollider2D attackHitBox;

    private float attackCooldownTimer = 0;

    private void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        GameInputActions.Instance.OnPlayerAttack += GameInputActions_OnPlayerAttack;
    }

    private void Update()
    {
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }

    private void GameInputActions_OnPlayerAttack(object sender, System.EventArgs e)
    {
        if (PlayerMovement.OnGround() && attackCooldownTimer <= 0)
        {
            var hits = Physics2D.BoxCastAll(attackHitBox.transform.position, attackHitBox.size, 0, Vector2.zero);
            foreach(var hit in hits)
            {
                Debug.Log(hit.transform.name);
            }

            attackCooldownTimer = attackCooldownMax;
            PlayerMovement.ResetVelocity();
            PlayerMovement.SetCanMove(false);
            OnPlayerPrimaryAttack?.Invoke(this, new OnPlayerPrimaryAttackEvent() { afterAttackCallback = () => { PlayerMovement.SetCanMove(true); } });
        }
    }
}
