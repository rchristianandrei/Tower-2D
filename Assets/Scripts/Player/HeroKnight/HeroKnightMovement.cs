using System;
using UnityEngine;
using UnityEngine.Rendering;

public class HeroKnightMovement : MonoBehaviour
{
    [SerializeField] private HeroKnight heroKnight;

    public event EventHandler<float> OnPlayerMoved;
    public event EventHandler OnPlayerJump;
    public event EventHandler OnPlayerLand;

    private void Start()
    {
        GameInputActions.Instance.OnPlayerJump += GameInputActions_OnPlayerJump;
    }

    private void LateUpdate()
    {
        var movDir = GameInputActions.Instance.getPlayerMovement();

        if (heroKnight.GetCanMove())
        {
            heroKnight.GetRigidbody2D().linearVelocity = new(movDir.x * heroKnight.GetMovementSpeed(), heroKnight.GetRigidbody2D().linearVelocity.y);
            OnPlayerMoved?.Invoke(this, movDir.x);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (heroKnight.OnGround())
        {
            OnPlayerLand?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInputActions_OnPlayerJump(object sender, EventArgs e)
    {
        if (!heroKnight.OnGround()) return;

        heroKnight.GetRigidbody2D().linearVelocity = new(heroKnight.GetRigidbody2D().linearVelocity.x, heroKnight.GetJumpForce());
        OnPlayerJump?.Invoke(this, EventArgs.Empty);
    }
}
