using UnityEngine;

public class EnemyWanderState : State
{
    private Enemy enemy;
    private Rigidbody2D rigid => enemy.rigid;

    [Header("Wander Settings")]
    public float wanderSpeed = 8f;      
    public float wanderRadius = 8f;    
    public float waitTime = 1.5f;      

    private Vector2 targetPosition;
    private float timer;
    private bool isIdle = true;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        // Initialize timer so he moves immediately or waits a bit
        timer = waitTime;
        PickNewTarget();
    }

    public override void LogicUpdate()
    {
        // State Logic: Idle vs Moving
        if (isIdle)
        {
            HandleIdle();
        }
        else
        {
            HandleMovement();
        }
    }

    void HandleIdle()
    {
        // Stop moving while thinking
        rigid.linearVelocity = Vector2.zero;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            PickNewTarget();
        }
    }

    void HandleMovement()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rigid.linearVelocity = direction * wanderSpeed;

        // Check if we reached the random spot
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Arrived, now wait
            isIdle = true;
            timer = waitTime;
        }
    }

    void PickNewTarget()
    {
        isIdle = false;

        // Pick a random point inside a circle around the CURRENT position
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        targetPosition = (Vector2)transform.position + randomPoint;
    }
}