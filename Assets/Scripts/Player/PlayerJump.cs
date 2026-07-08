using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public float gravity = 25f;
    public Transform visualRoot;

    private Animator animator;
    public bool isAirborne { get; private set; } = false;

    private float verticalVelocity = 0f;
    private float currentHeight = 0f; // Refeencia a la altura actual del personaje
    private float currentFloorHeight = 0f; // Referencia a la altura del suelo del cual hemos slatado, y al que querremos aterrizar

    void Awake()
    {
        if (visualRoot != null)
        {
            animator = visualRoot.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isAirborne)
        {
            StartJump();
        }

        if (isAirborne || currentHeight > currentFloorHeight)
        {
            HandleAirbornePhysics();
        }
    }

    private void StartJump()
    {
        isAirborne = true;
        verticalVelocity = jumpForce;

        // Guardamos la altura actual del suelo antes de saltar para saber dónde aterrizar
        currentFloorHeight = transform.position.y;
        currentHeight = currentFloorHeight;

        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
    }

    private void HandleAirbornePhysics()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        currentHeight += verticalVelocity * Time.deltaTime;

        if (verticalVelocity < 0) // If falling
        {
            CheckForLanding();
        }

        // Move the sprite's position up and down based on currentHeight to simulate jump
        transform.localPosition = new Vector2(transform.position.x, currentHeight);

    }

    private void CheckForLanding()
    {
        if (currentHeight <= currentFloorHeight)
        {
            Land(currentFloorHeight);
        }
    }

    private void Land(float targetFloorHeight)
    {
        currentHeight = targetFloorHeight;
        currentFloorHeight = targetFloorHeight;
        verticalVelocity = 0f;
        isAirborne = false;

        transform.position = new Vector2(transform.position.x, currentHeight);
        
    }

    //private void CheckWalkOffLedge()
    //{
    //    if (currentFloorHeight > 0)
    //    {
    //        bool overPlatform = false;
    //        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);

    //        //foreach (var hit in hits)
    //        //{
    //        //    if (hit.GetComponent<PlatformBase>() != null)
    //        //    {
    //        //        overPlatform = true;
    //        //        break;
    //        //    }
    //        //}

    //        if (!overPlatform)
    //        {
    //            currentFloorHeight = 0f;
    //            isAirborne = true;
    //            verticalVelocity = 0f;
    //        }
    //    }
    //}
}