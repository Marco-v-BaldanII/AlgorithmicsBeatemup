using UnityEngine;

public class EnemyIdleState : State
{

    private Enemy enemy;
    private Rigidbody2D rigid;

    public float chaseDistance = 7f;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>(); // reference to the enemy
        rigid = GetComponentInParent<Rigidbody2D>(); // reference to the enemy's Rigidbody2D
    }


    public override void LogicUpdate()
    {
        rigid.linearVelocity = Vector2.zero; // don't move

        // Check proximity to player
        if (Vector2.Distance(transform.position, enemy.player.transform.position) <= chaseDistance)
        {
            //currentState = LongmoanState.Chase;
            TransitionTo("EnemyChaseState"); // notify state machine to transition
        }

    }

}
