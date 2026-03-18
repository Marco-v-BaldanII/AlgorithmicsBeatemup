using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    public float duracionFlash = 0.5f;

    private CinemachineImpulseSource cameraShake;
    private SpriteRenderer spriteRenderer;

    Color colorHurt = new Color(0.73f, 45f / 255f, 68f / 255f, 0.5f);
    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();    
    }


    private IEnumerator RedTintRoutine()
    {

        spriteRenderer.color = Color.red;


        yield return new WaitForSeconds(0.5f);


        spriteRenderer.color = Color.white;


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {

            health--;
            cameraShake.GenerateImpulse();

            if (health <= 0)
            {
                print("Player Defeated");
                // Reload scene, MAKE sure scene index exists
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                StartCoroutine(FlashColor());
            }


        }

        else if(collision.CompareTag("MagicPotion")) {

            PlayerMagic magic = GetComponent<PlayerMagic>();
            if (magic != null)
            {
                magic.AddPotion();
                Destroy(collision.gameObject);
            }

        }
    }


    private IEnumerator FlashColor()
    {
        float tiempo = 0;

        while (tiempo < duracionFlash)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracionFlash;

            spriteRenderer.color = Color.Lerp(Color.white, colorHurt, progreso);
            yield return null; 
        }

        tiempo = 0;


        while (tiempo < duracionFlash)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracionFlash;
            spriteRenderer.color = Color.Lerp( colorHurt, Color.white, progreso);
            yield return null;
        }

        spriteRenderer.color = Color.white;
    }

}
