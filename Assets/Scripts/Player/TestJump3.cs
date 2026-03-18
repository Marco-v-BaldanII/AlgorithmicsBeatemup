using UnityEngine;

public class TestJump3 : MonoBehaviour
{

    // EL siguiente script debería detener la caida del salto cuando llegamos a la misma posisicón desde la cual empezamos el salto

    [Range(-20f, 0)] public float gravity;

    public float startingJumpForce = 10f;
    public float verticalVelocity;

    bool isAirborne = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            isAirborne = true;

        }

        if (isAirborne)
        {
            transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
            verticalVelocity += gravity * Time.deltaTime;
        }
    }
}
