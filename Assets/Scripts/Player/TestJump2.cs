using UnityEngine;

public class TestJump2 : MonoBehaviour
{

    float startingJumpForce = 10f;
    public float verticalVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        verticalVelocity = startingJumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += Vector3.up * verticalVelocity * Time.deltaTime;


        verticalVelocity += Physics2D.gravity.y * Time.deltaTime;

    }
}
