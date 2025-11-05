using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackDamage = 10f;
    public float attackCooldown = 0.5f; 
    private float nextAttackTime = 0f;

    [Header("References")]
    public Collider2D attackHitbox;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        // Ensure the attack hitbox is off by default
        if (attackHitbox != null)
        {
            attackHitbox.enabled = false;
        }
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        if (attackHitbox != null)
        {
            StartCoroutine(HitboxActiveDuration(0.1f));
        }
    }

    IEnumerator HitboxActiveDuration(float duration)
    {
        attackHitbox.enabled = true; // Activate the hitbox
        yield return new WaitForSeconds(duration);
        attackHitbox.enabled = false; // Deactivate the hitbox
    }

    //  Handle collision with the attack hitbox
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Empty for now
    }
}