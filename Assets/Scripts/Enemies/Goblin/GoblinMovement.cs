using System;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    [SerializeField] private EnemyArea enemyArea;
    [SerializeField] private float closeToTargetDistance = 2f;

    [SerializeField] private Goblin goblin;
    [SerializeField] private Vector3 position;

    public event EventHandler<OnGoblinMovedEvent> OnGoblinMoved;
    public class OnGoblinMovedEvent : EventArgs
    {
        public Vector2 direction;
    }

    private Rigidbody2D rb2D;

    private GameObject target;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemyArea.OnNewTarget += (object sender, EnemyArea.OnNewTargetEvent e) =>
        {
            target = e.target;
        };

        enemyArea.OnTargetLost += (object sender, EnemyArea.OnTargetLostEvent e) =>
        {
            if(e.target == target) target = null;
        };
    }

    private void Update()
    {
        Vector2 dir = Vector2.zero;

        if (target != null) {
            var distance = Vector2.Distance(target.transform.position, transform.position);

            if (distance > closeToTargetDistance)
            {
                dir = (target.transform.position - transform.position).normalized;
                rb2D.linearVelocity = new(dir.x * goblin.GetMovementSpeed(), rb2D.linearVelocity.y);
            }
        };

        OnGoblinMoved?.Invoke(this, new OnGoblinMovedEvent { direction = dir });
    }
}
