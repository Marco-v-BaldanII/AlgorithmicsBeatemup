using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class EnemyWanderState : State
{
    private Enemy enemy;
    private Rigidbody2D rigid => enemy.rigid;

    [Header("Wander Settings")]
    public float wanderSpeed = 8f;
    public float wanderRadius = 8f;
    public float waitTime = 1.5f;

    public Collider2D levelWalls;

    private Vector2 targetPosition;
    private float timer;
    private bool isIdle = true;
    private bool isPickingTarget = false;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        // Initialize timer so he moves immediately or waits a bit
        timer = waitTime;
        StartCoroutine(PickNewTarget());

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

        if (timer <= 0f && isPickingTarget == false)
        {
            StartCoroutine(PickNewTarget());
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


    IEnumerator PickNewTarget()
    {
        isPickingTarget = true;

        // Pick a random point inside a circle around the CURRENT position
        do
        {
            Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
            targetPosition = (Vector2)transform.position + randomPoint;

            yield return null; // Wait one frame

        } while (levelWalls.OverlapPoint(targetPosition) == true); // If the target position is inside wall, pick another one



        isPickingTarget = false;
        // Change isIdle so that the function HandleMovement is now called
        isIdle = false;
    }
}