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
    private float currentHeight = 0f;
    private float currentFloorHeight = 0f;

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
        //else
        //{
        //    CheckWalkOffLedge();
        //}
    }

    private void StartJump()
    {
        isAirborne = true;
        verticalVelocity = jumpForce;

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
        visualRoot.localPosition = new Vector3(0, currentHeight, 0);
        
    }

    private void CheckForLanding()
    {
        float targetFloorHeight = 0f;

        //Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        //foreach (var hit in hits)
        //{
        //    PlatformBase platform = hit.GetComponent<PlatformBase>();
        //    if (platform != null && currentHeight >= platform.surfaceHeight)
        //    {
        //        targetFloorHeight = platform.surfaceHeight;
        //    }
        //}

        if (currentHeight <= targetFloorHeight)
        {
            Land(targetFloorHeight);
        }
    }

    private void Land(float targetFloorHeight)
    {
        currentHeight = targetFloorHeight;
        currentFloorHeight = targetFloorHeight;
        verticalVelocity = 0f;
        isAirborne = false;

        if (visualRoot != null)
        {
            visualRoot.localPosition = new Vector3(0, currentHeight, 0);
        }
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