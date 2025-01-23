using Mono.Cecil;
using System;
using UnityEngine;

public class HeroKnightAttack : MonoBehaviour
{
    public event EventHandler<OnPlayerPrimaryAttackEvent> OnPlayerPrimaryAttack;
    public class OnPlayerPrimaryAttackEvent : EventArgs
    {
        public Action afterAttackCallback;
    }

    [SerializeField] private HeroKnight heroKnight;

    [SerializeField] private float attackCooldownMax = 0.75f;
    [SerializeField] private BoxCollider2D attackHitBox;

    private float attackCooldownTimer = 0;

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
        if (heroKnight.OnGround() && attackCooldownTimer <= 0)
        {
            var hits = Physics2D.BoxCastAll(attackHitBox.transform.position, attackHitBox.size, 0, Vector2.zero);
            foreach(var hit in hits)
            {
                DealDamage(hit.transform.gameObject, 10f);
            }

            attackCooldownTimer = attackCooldownMax;
            heroKnight.ResetLinearVelocity();
            heroKnight.SetCanMove(false);
            OnPlayerPrimaryAttack?.Invoke(this, new OnPlayerPrimaryAttackEvent() { afterAttackCallback = () => { heroKnight.SetCanMove(true); } });
        }
    }

    private void DealDamage(GameObject target, float damage)
    {
        if(target.TryGetComponent<IAttackable>(out var attackable))
        {
            attackable.ReceiveAttack(gameObject, damage);
        }
    }
}
