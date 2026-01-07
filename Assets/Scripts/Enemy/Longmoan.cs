using UnityEngine;


public enum LongmoanState
{
    Idle   = 0,
    Attack = 1,
    Chase  = 2,
}

public class Longmoan : Enemy
{

    public PlayerMovement player;

    private LongmoanState currentState = LongmoanState.Idle;

    public float chaseDistance = 7f;

    public float chaseSpeed = 4f;

    public float atkDistance = 2f;

    protected override void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        base.Awake(); // Call enemy awake method
    }


    private void Update()
    {
        switch (currentState)
        {
            case LongmoanState.Idle:
                HandleIdleState();
                break;
            case LongmoanState.Attack:
                HandleAttackState();
                break;
            case LongmoanState.Chase:
                HandleChaseState();
                break;
        }

        HandleDirection();
    }

    void HandleIdleState()
    {
        rigid.linearVelocity = Vector2.zero; // don't move

        // Check proximity to player
        if ( Vector2.Distance( transform.position , player.transform.position ) <= chaseDistance) 
        {
            currentState = LongmoanState.Chase;
        }

    }

    float attackTimer = 2f;
    float attackRate = 2f;
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
            currentState = LongmoanState.Chase;
        }
    }


    void HandleChaseState()
    {
        //                             
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        //   .velocity in older versions of Unity

        rigid.linearVelocity = directionToPlayer * chaseSpeed;

        if(Vector2.Distance(transform.position, player.transform.position) <= atkDistance)
        {
            currentState = LongmoanState.Attack;
        }
    }

}
