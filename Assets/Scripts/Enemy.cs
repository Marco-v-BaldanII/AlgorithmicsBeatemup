using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 2;

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
