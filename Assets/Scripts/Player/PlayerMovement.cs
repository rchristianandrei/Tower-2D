using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayerMask;

    private new Rigidbody2D rigidbody;
    private new CapsuleCollider2D collider;

    public event EventHandler<float> OnPlayerMoved;
    public event EventHandler OnPlayerJump;
    public event EventHandler OnPlayerLand;

    private bool canMove = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        GameInputActions.Instance.OnPlayerJump += GameInputActions_OnPlayerJump;
    }

    private void LateUpdate()
    {
        var movDir = GameInputActions.Instance.getPlayerMovement();

        if (canMove)
        {
            rigidbody.linearVelocity = new(movDir.x * movementSpeed, rigidbody.linearVelocity.y);
            OnPlayerMoved?.Invoke(this, movDir.x);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnGround())
        {
            OnPlayerLand?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInputActions_OnPlayerJump(object sender, EventArgs e)
    {
        if (!OnGround()) return;

        rigidbody.linearVelocity = new(rigidbody.linearVelocity.x, jumpForce);
        OnPlayerJump?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Returns if the player is on ground or not.
    /// </summary>
    /// <returns></returns>
    public bool OnGround()
    {
        var distance = 0f;
        var size = new Vector2(collider.size.x, 0.01f);
        var hit = Physics2D.BoxCast(transform.position, size, 0, Vector2.down, distance, groundLayerMask);

        return hit.collider != null;
    }

    public void ResetVelocity()
    {
        rigidbody.linearVelocity = Vector2.zero;
    }

    #region "Getters and Setters"
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public bool GetCanMove()
    {
        return this.canMove;
    }
    #endregion
}
