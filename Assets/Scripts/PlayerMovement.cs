using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; 

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator; 

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 movementVector = new Vector2(horizontalInput, verticalInput).normalized;
        rigid.linearVelocity = movementVector * moveSpeed;

        HandleDirection(horizontalInput, movementVector);
        HandleDepthSorting();
    }


    private void HandleDirection(float horizontalInput, Vector2 movementVector)
    {
        // Flip the sprite to face the direction of movement
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = true; // Facing Right
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = false;  // Facing Left
        }

        if (animator != null)
        {
            bool isMoving = movementVector.magnitude > 0;
            animator.SetBool("IsRunning", isMoving);
        }
    }

    private void HandleDepthSorting()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}