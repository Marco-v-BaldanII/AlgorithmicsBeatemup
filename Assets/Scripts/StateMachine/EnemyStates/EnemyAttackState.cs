using UnityEngine;

public class EnemyAttackState : State
{

    private Enemy enemy;
    private Rigidbody2D rigid;
    private Animator animator => enemy.animator;
    private PlayerMovement player => enemy.player;

    public float chaseSpeed = 4f;
    public float atkDistance = 2f;
    float attackTimer = 2f;
    float attackRate = 2f;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>(); // reference to the enemy
        rigid = GetComponentInParent<Rigidbody2D>(); // reference to the enemy's Rigidbody2D
    }

    public override void LogicUpdate()
    {
        HandleAttackState();
    }

    void HandleAttackState()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer < 0f)
        {
            animator.SetTrigger("Attack");
            attackTimer = attackRate;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        // If player is far go back to chasing
        if (distance > atkDistance)
        {
            TransitionTo("EnemyChaseState");
            //currentState = LongmoanState.Chase;
        }
    }


}
