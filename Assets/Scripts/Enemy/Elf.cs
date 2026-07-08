using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Elf : Enemy
{
    public GameObject potionPrefab;


    List<Potion> potionList = new List<Potion>();

    private void Start()
    {
        for (int i = 0; i < hp; ++i)
        {
            GameObject potionObject = Instantiate(potionPrefab, transform.position, Quaternion.identity);
            Potion potion = potionObject.GetComponent<Potion>();
            potion.gameObject.SetActive(false); // Start with potions inactive

            potionList.Add(potion);
        }
    }

    protected override void ReceiveDamage()
    {
        hp--;

        potionList[hp].gameObject.SetActive(true);
        potionList[hp].transform.position = transform.position;

        // We don't delete enemy here, it will delete itself after running off
    }

    private void Update()
    {
        HandleDirection();
    }

    protected override void ReceiveKnockBack()
    {
        // Transition to Hit State
        animator.SetTrigger("Hit");
        if (hp <= 0)
        {
            // If hp is below 0 then run off
            stateMachine.OnChildTransition(stateMachine.CurrentState, "EnemyEscapeState");
        }
        else
        {
            stateMachine.OnChildTransition(stateMachine.CurrentState, "EnemyHitState", new() { ["hit_mode"] = "knockback" });
        }
    }
}
