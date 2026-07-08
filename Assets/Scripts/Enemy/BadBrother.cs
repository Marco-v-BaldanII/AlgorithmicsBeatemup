using UnityEngine;

public class BadBrother : Enemy
{
    protected void Update()
    {
        HandleDirection();
        
    }

    protected override void HandleDirection()
    {
        // Flip the sprite to face the direction of movement
        if (rigid.linearVelocityX > 0)
        {
            transform.localScale = new Vector2(-1, 1); // Facing Right
        }
        else if (rigid.linearVelocityX < 0)
        { 
            transform.localScale = new Vector2(+1, 1);  // Facing Left
        }

        animator.SetFloat("YVelocity", rigid.linearVelocity.magnitude);
    }
} 
