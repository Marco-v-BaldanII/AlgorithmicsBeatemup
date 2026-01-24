using UnityEngine;
using System.Collections.Generic;

public class EnemyEscapeState : State
{
    private Enemy enemy;
    private Rigidbody2D rigid;

    [Header("Escape Settings")]
    public float escapeSpeed = 7f;
    public float timeBeforeDelete = 5f;

    private float runDirection;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        rigid = GetComponentInParent<Rigidbody2D>();
    }

    public override void Enter(Dictionary<string, object> extraArgs = null)
    {
        float diff = transform.position.x - enemy.player.transform.position.x;
        runDirection = Mathf.Sign(diff);

        if (runDirection == 0) runDirection = 1;

        Physics2D.IgnoreCollision(GetComponentInParent<Collider2D>(), enemy.player.GetComponent<Collider2D>(), true);

        Destroy(enemy.gameObject, timeBeforeDelete);
    }

    public override void LogicUpdate()
    {
        rigid.linearVelocity = new Vector2(runDirection * escapeSpeed, 0);

        if (transform.localScale.x != 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * runDirection;
            transform.localScale = newScale;
        }
    }

    public override void Exit()
    {
        rigid.linearVelocity = Vector2.zero;
    }
}