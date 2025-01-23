using Unity.VisualScripting;
using UnityEngine;

public class HeroKnight : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] private float movementSpeed = 5f;
    public float GetMovementSpeed() { return movementSpeed; }
    public void SetMovementSpeed(float speed) {  movementSpeed = speed; }

    [SerializeField] private float jumpForce = 5f;
    public float GetJumpForce() {  return jumpForce; }
    public void SetJumpForce(float force) {  jumpForce = force; }

    [SerializeField] private LayerMask groundLayerMask;

    // Private Fields
    private Rigidbody2D rb2D;
    public Rigidbody2D GetRigidbody2D() { return rb2D; }

    private new CapsuleCollider2D collider;

    private bool canMove = true;
    public bool GetCanMove() { return canMove; }
    public void SetCanMove(bool canMove) { this.canMove = canMove; }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
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

    /// <summary>
    /// Resets the Linear Velocity of the Rigidbody2D.
    /// </summary>
    public void ResetLinearVelocity()
    {
        rb2D.linearVelocity = Vector2.zero;
    }
}
