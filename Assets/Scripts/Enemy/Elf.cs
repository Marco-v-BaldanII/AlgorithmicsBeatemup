using UnityEngine;

public class Elf : Enemy
{
    protected override void ReceiveDamage()
    {
        hp--;
        // We don't delete enemy here, it will delete itself after running off
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
