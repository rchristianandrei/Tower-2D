using Unity.VisualScripting;
using UnityEngine;

public class Goblin : MonoBehaviour, IAttackable
{
    [SerializeField] private float movementSpeed = 2.5f;
    public float GetMovementSpeed() {  return movementSpeed; }
    public void SetMovementSpeed(float speed) {  movementSpeed = speed; }

    public void ReceiveAttack(GameObject sender, float damage)
    {
        Debug.Log($"Receive {damage} Damage from {sender.name}");
    }
}
