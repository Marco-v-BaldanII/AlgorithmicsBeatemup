using UnityEngine;

public class Enemy : MonoBehaviour
{
    // All enemies have hp and access to this rigid body
    public int hp = 2;
    protected Rigidbody2D rigid;
    protected Animator animator;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
