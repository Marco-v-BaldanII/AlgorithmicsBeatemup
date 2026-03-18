using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 20f;
    public float lifetime = 3f;
    public Vector3 moveDirection = Vector3.right;

    [Header("Combat")]
    public float damage = 50f;

    void Start()
    {
        // Auto-destroy to prevent memory leaks
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }


}