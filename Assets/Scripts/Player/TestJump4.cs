using UnityEngine;

public class TestJump4 : MonoBehaviour
{

    // EL siguiente script debería detener la caida del salto cuando llegamos a la misma posisicón desde la cual empezamos el salto

    [Range(-20f, 0)] public float gravity;

    public float startingJumpForce = 10f;
    public float verticalVelocity;

    private bool isAirborne = false;

    private float targetFloorHeight = 0f;

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
            targetFloorHeight = transform.position.y; // Guardamos la posición en y en la que estabamos antes de iniciar el salto
        }

        if (isAirborne)
        {

            HandleAirbornePhysics();

        }
    }


    private void HandleAirbornePhysics()
    {
        transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
        verticalVelocity += gravity * Time.deltaTime;


        if (verticalVelocity < 0) // If falling
        {
            // Si estamos cayendo hemos de controlar que aterrizemos
        }


    }

}
