using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    EnemySO SO;

    Player player;
    public NavMeshAgent agent;
    Animator animator;
    public Camera cam;
    enemy enemyScript;
    Room parentRoom;
    public bool path;

    EnemyAttack attackScript;

    private void Start()
    {
        

        enemyScript = GetComponent<enemy>();
        attackScript = GetComponent<EnemyAttack>();
        player = FindObjectOfType<Player>();

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        parentRoom = enemyScript.GetSpawnedFrom().GetComponentInParent<Room>(); // get the parent room throught the spawner

        SO = enemyScript.SO;
        agent.speed = SO.speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void FixedUpdate()
    {

        path = agent.hasPath;
        if (player.transform != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= SO.maxSight)
            {
                FollowPlayer();

            }
            else
            {
                animator.SetBool("isRunning", false);
                if (Vector2.Distance(agent.destination , transform.position)<agent.stoppingDistance) // reached its target
                {
                    // give it a random destination
                    agent.SetDestination(parentRoom.GetRandomPosInRoom());

                }
            }
        }

    }

    void FollowPlayer()
    {
        
        animator.SetBool("isRunning", true);
        agent.SetDestination(player.transform.position);
        attackScript.CheckForAttack();

    }

}
