using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherIdle : State
{

    public override void Enter(Dictionary<string, object> extraArgs = null)
    {
        StartCoroutine("WaitForOnlyEnemy");
    }

    private IEnumerator WaitForOnlyEnemy()
    {
        yield return new WaitForSeconds(0.5f);

        while (EnemyManager.instance.activeEnemies.Count != 0)
        {
            yield return new WaitForSeconds(0.5f);

        }

        TransitionTo("EnemyChaseState");

    }

}
