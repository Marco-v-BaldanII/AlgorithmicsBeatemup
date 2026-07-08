using UnityEngine;
using System.Collections;

public class PlayerMagic : MonoBehaviour
{
    [Header("Magic Settings")]
    public int currentPotions = 0;
    public int maxPotions = 6;
    public KeyCode magicKey = KeyCode.M;
    public int minPotionsForSpecial = 3;



    [Header("VFX & Combat")]
    public GameObject magicPrefab;
    public Transform spawnPoint;
    public float specialDuration = 1.5f;

    void Update()
    {
        if (Input.GetKeyDown(magicKey))
        {
            TryCastMagic();
        }
    }

    public void AddPotion()
    {
        if (currentPotions < maxPotions)
        {
            currentPotions++;
            Debug.Log("Potion added. Total: " + currentPotions);
        }
    }

    private void TryCastMagic()
    {
        if (currentPotions >= minPotionsForSpecial)
        {
            StartCoroutine(CastSpecialAttack());
        }
        else
        {
            Debug.Log("Not enough potions!");
        }
    }

    private IEnumerator CastSpecialAttack()
    {
        // Consume all potions like in Golden Axe
        currentPotions = 0;

        if (magicPrefab != null && spawnPoint != null)
        {
            Instantiate(magicPrefab, transform.position + new Vector3(-10,0), Quaternion.identity);
        }

        // Wait for the attack animation/effect to finish
        yield return new WaitForSeconds(specialDuration);
    }
}