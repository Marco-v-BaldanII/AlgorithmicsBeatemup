using System.Collections.Generic;
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

    public override void Enter(Dictionary<string, object> extraArgs = null)
    {
        if (EnemyManager.instance.activeEnemies.Contains(enemy))
        {
            EnemyManager.instance.activeEnemies.Remove(enemy); // Cuando el enemigo entra en idle, no esta activo, asi que lo quitamos de la lista
        }

    }


    public override void Exit()
    {
        base.Exit();
        // Cuando el enemigo deja de estar en idle, esta activo, asi que lo añadimos a la lista
        if (EnemyManager.instance.activeEnemies.Contains(enemy) == false)
        {
            EnemyManager.instance.activeEnemies.Add(enemy);
        }
    }

}
