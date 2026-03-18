using UnityEngine;

public class Elf : Enemy
{
    public GameObject potionPrefab;

    protected override void ReceiveDamage()
    {
        hp--;

        Instantiate(potionPrefab, transform.position, Quaternion.identity);

        // We don't delete enemy here, it will delete itself after running off
    }

    private void Update()
    {
        HandleDirection();
    }

    protected override void ReceiveKnockBack()
    {
        // Transition to Hit State
        animator.SetTrigger("Hit");
        if (hp <= 0)
        {
            // If hp is below 0 then run off
            stateMachine.OnChildTransition(stateMachine.CurrentState, "EnemyEscapeState");
        }
        else
        {
            stateMachine.OnChildTransition(stateMachine.CurrentState, "EnemyHitState", new() { ["hit_mode"] = "knockback" });
        }
    }
}
