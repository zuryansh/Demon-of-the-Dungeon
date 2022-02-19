using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking;
    Player player;
    public int damage = 5;
    enemy enemyScript;
    EnemyMovement movementScript;
    NavMeshAgent agent;

    private void Start()
    {
        enemyScript = GetComponent<enemy>();
        movementScript = GetComponent<EnemyMovement>();
        agent = movementScript.agent;
        player = enemyScript.player;
    }


    public void CheckForAttack()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
        {//in range
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {


        isAttacking = true;
        enemyScript.animator.SetTrigger("Attack");// trigger attack warning
        yield return new WaitForSeconds(0.3f); // wait for warning to go away
        StartCoroutine(player.TakeDamage(damage));// damage player
        yield return new WaitForSeconds(1.5f);// wait for attack cooldown
        isAttacking = false;

    }
}
