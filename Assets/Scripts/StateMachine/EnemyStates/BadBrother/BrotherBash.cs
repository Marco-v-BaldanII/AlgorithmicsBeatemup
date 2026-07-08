using UnityEngine;
using System;
using System.Collections.Generic;

public class BrotherBash : State
{
    private Enemy enemy;
    private Rigidbody2D rigid => enemy.rigid;

    public float sprintSpeed = 10f;
    public float bashDuration = 1.3f;

    public float spirntAcceleration = 1.2f;

    float startingSpeed;

    void Start()
    {
        startingSpeed = sprintSpeed;
        enemy = GetComponentInParent<Enemy>(); // reference to the enemy
    }

    public override void Enter(Dictionary<string, object> extraArgs = null) {

        enemy = GetComponentInParent<Enemy>(); // reference to the enemy
        Invoke("GoToIdle", bashDuration);
        enemy.animator.SetTrigger("Bash");
    }

    public override void LogicUpdate()
    {
        Vector2 directionToPlayer = (enemy.player.transform.position - transform.position).normalized;
        //   .velocity in older versions of Unity

        rigid.linearVelocity = directionToPlayer * sprintSpeed;   
        sprintSpeed += spirntAcceleration * Time.deltaTime;
    }

    public void GoToIdle()
    {
        TransitionTo("EnemyIdleState");
    }

    public override void Exit() { 

        sprintSpeed = startingSpeed;

      }

}
