using UnityEngine;

public class Enemy : MonoBehaviour
{
    // All enemies have hp and access to these variables
    public int hp = 2;
    protected Rigidbody2D rigid;
    protected Animator animator;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected void HandleDirection()
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

        void ReceiveDamage()
    {
        hp--;

        if (hp <= 0)
        {
           Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            ReceiveDamage();
        }
    }

    void Die()
    {
       Destroy(gameObject);
    }


    void AlignYToTarget()
    {

    }

}
