using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnDistance = 14f;

    private PlayerMovement player;

    private GameObject enemy;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();

        // Spawn enemy at the beginning, then deactivate (less expensive)

        enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        enemy.SetActive(false);
    }

    private void Update()
    {

        // Detect when player is close

        if (Mathf.Abs(transform.position.x - player.transform.position.x ) <= spawnDistance)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }

    }


}
