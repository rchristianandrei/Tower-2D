using UnityEngine;

public interface IAttackable
{
    public void ReceiveAttack(GameObject sender, float damage);
}
