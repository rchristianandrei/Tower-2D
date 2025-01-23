using System;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    [SerializeField] private Goblin goblin;

    public event EventHandler OnGoblinAttack;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnGoblinAttack?.Invoke(this, EventArgs.Empty);
        }
    }
}
