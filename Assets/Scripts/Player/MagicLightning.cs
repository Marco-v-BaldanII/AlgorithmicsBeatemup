using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 20f;
    public float lifetime = 3f;
    public Vector3 moveDirection = Vector3.right;

    void Start()
    {
        // Auto-destroy
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }


}