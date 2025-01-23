using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    private readonly List<GameObject> targets = new();
    public List<GameObject> GetTargets() {  return new List<GameObject>(targets); }

    public event EventHandler<OnNewTargetEvent> OnNewTarget;
    public class OnNewTargetEvent : EventArgs
    {
        public GameObject target;
    }

    public event EventHandler<OnTargetLostEvent> OnTargetLost;
    public class OnTargetLostEvent : EventArgs
    {
        public GameObject target;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddTarget(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveTarget(collision.gameObject);
    }

    public void AddTarget(GameObject target)
    {
        if (targets.Contains(target)) return;

        targets.Add(target);
        OnNewTarget?.Invoke(this, new OnNewTargetEvent { target = target });
    }

    public void RemoveTarget(GameObject target)
    {
        if (!targets.Contains(target)) return;
        targets.Remove(target);
        OnTargetLost?.Invoke(this, new OnTargetLostEvent { target = target });
    }
}
