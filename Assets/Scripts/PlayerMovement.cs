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
        // Read Input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Move based on Input
        Vector2 movementVector = new Vector2(horizontalInput, verticalInput).normalized;
        rigid.linearVelocity = movementVector * moveSpeed;

        HandleDirection(horizontalInput);
    }


    private void HandleDirection(float horizontalInput)
    {
        // Flip the sprite to face the direction of movement
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector2(-1, 1); // Facing Right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector2( 1, 1);  // Facing Left
        }
    }

    private void HandleDepthSorting()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}