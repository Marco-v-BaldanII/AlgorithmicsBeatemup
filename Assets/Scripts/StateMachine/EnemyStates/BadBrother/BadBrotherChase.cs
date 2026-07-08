using System.Collections.Generic;
using UnityEngine;

public class BadBrotherChase : EnemyChaseState
{
    public float minTackleTime = 1.2f;
    public float maxTackleTime = 3f;

    public override void Enter(Dictionary<string, object> extraArgs = null) {

        Invoke("ChangeToTackle", Random.Range(minTackleTime, maxTackleTime));
        
    }

    private void ChangeToTackle()
    {
        if (enemy.stateMachine.NameCurrentState == name)
        {
            TransitionTo("BadBrotherBash");
        }
    }
}
