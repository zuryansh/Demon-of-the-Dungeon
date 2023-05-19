using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    EnemySO SO;
    public bool isAttacking;
    Player player;
    enemy enemyScript;
    EnemyMovement movementScript;
    NavMeshAgent agent;

    private void Start()
    {
        enemyScript = GetComponent<enemy>();
        movementScript = GetComponent<EnemyMovement>();
        agent = movementScript.agent;
        player = enemyScript.player;

        SO = enemyScript.SO;
        if (SO.enemyType == EnemySO.EnemyType.Bomber)
        {
            StartCoroutine(SpawnBomb());
        }
    }

    

    public void CheckForAttack()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
        {//in range
            if (!isAttacking)
            {
                if (SO.enemyType == EnemySO.EnemyType.Melle)
                    StartCoroutine(MelleAttack());
                else if (SO.enemyType == EnemySO.EnemyType.Ranged)
                    StartCoroutine(RangedAttack());
            }
        }
    }

    public IEnumerator  SpawnBomb()
    {
        yield return new WaitForSeconds(SO.attackCooldown);
        isAttacking = true;
        enemyScript.animator.SetTrigger("Attack"); // trigger attack warning 

        yield return new WaitForSeconds(0.3f);  // wait for warning to go away

        Instantiate(SO.bomb, transform.position, Quaternion.identity); // spawn a bomb

        isAttacking = false;
        StartCoroutine(SpawnBomb());
        
        
    }

    IEnumerator RangedAttack()
    {
        //get dir of player
        Vector2 dir = player.transform.position - transform.position;
        // lauch a projectile

        isAttacking = true;
        enemyScript.animator.SetTrigger("Attack"); // trigger attack warning 

        yield return new WaitForSeconds(0.3f);  // wait for warning to go away

        Projectile obj = Instantiate(SO.projectile , transform.position , Quaternion.identity).GetComponent<Projectile>();
        
        obj.velocity = dir.normalized*SO.projectileForce;
        obj.damage = SO.damage;

        yield return new WaitForSeconds(SO.attackCooldown);// wait for attack cooldown
        isAttacking = false;

    }

    IEnumerator MelleAttack()
    {

        
        isAttacking = true;
        enemyScript.animator.SetTrigger("Attack");// trigger attack warning
        yield return new WaitForSeconds(0.3f); // wait for warning to go away

        if (Vector2.Distance(player.transform.position, transform.position) <= agent.stoppingDistance)
        {
            
            StartCoroutine(player.TakeDamage(SO.damage));// damage player
            yield return new WaitForSeconds(1.5f);// wait for attack cooldown
            isAttacking = false;
        }


    }
}
