using UnityEngine;

public class GoblinAnimation : MonoBehaviour
{
    [SerializeField] private GoblinMovement movement;
    [SerializeField] private GoblinAttack attack;

    private const string IS_RUNNING = "IsRunning";
    private const string ATTACK = "Attack";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        movement.OnGoblinMoved += Movement_OnGoblinMoved;
        attack.OnGoblinAttack += Attack_OnGoblinAttack;
    }

    private void Attack_OnGoblinAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    private void Movement_OnGoblinMoved(object sender, GoblinMovement.OnGoblinMovedEvent e)
    {
        if(e.direction.x > 0)
        {
            transform.localScale = new (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            animator.SetBool(IS_RUNNING, true);
        }
        else if (e.direction.x < 0)
        {
            transform.localScale = new (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            animator.SetBool(IS_RUNNING, true);
        }
        else
        {
            animator.SetBool(IS_RUNNING, false);
        }
    }
}
