using UnityEngine;

public class Goblin : MonoBehaviour, IAttackable
{
    public void ReceiveAttack(GameObject sender, float damage)
    {
        Debug.Log($"Receive {damage} Damage from {sender.name}");
    }
}
