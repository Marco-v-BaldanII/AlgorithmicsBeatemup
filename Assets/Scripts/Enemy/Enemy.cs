using UnityEngine;

public class Enemy : MonoBehaviour
{
    // All enemies have hp and access to these variables
    public int hp = 2;
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer sprite;
    public PlayerMovement player;
    public StateMachine stateMachine;

    protected virtual void Awake()
    {
        rigid = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();
        stateMachine = GetComponentInChildren<StateMachine>();
    }

    protected virtual void HandleDirection()
    {
        // Flip the sprite to face the direction of movement
        if (rigid.linearVelocityX > 0)
        {
            transform.localScale = new Vector2(1, 1); // Facing Right
        }
        else if (rigid.linearVelocityX < 0)
        {
            transform.localScale = new Vector2(-1, 1);  // Facing Left
        }

        animator.SetFloat("YVelocity", rigid.linearVelocityY);
    }

    protected virtual void ReceiveDamage()
    {
        AudioManager.instance.PlayMusic("Level3Theme");

        hp--;

        if (hp <= 0)
        {
            AudioManager.instance.PlaySfx("EnemyDeath");
            Die();
        }
        else
        {
            AudioManager.instance.PlaySfx("EnemyHit");
        }
    }

    protected virtual void ReceiveKnockBack()
    {
        // Transition to Hit State
        animator.SetTrigger("Hit");
        stateMachine.OnChildTransition(stateMachine.CurrentState,"EnemyHitState", new() { ["hit_mode"] = "knockback" });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            ReceiveDamage();
            ReceiveKnockBack();
        }
    }

    void Die()
    {
       Destroy(gameObject);
    }


    void AlignYToTarget()
    {

    }

    private void OnDestroy()
    {
        // Por si acaso al destruir el enemigo comprobamos si esta en la lista y lo quitamos,
        // de lo contrario tener un elemento "null" en la lista puede dar problemas
        if (EnemyManager.instance.activeEnemies.Contains(this) == true)
        {
            EnemyManager.instance.activeEnemies.Remove(this);
        }
    }

}
