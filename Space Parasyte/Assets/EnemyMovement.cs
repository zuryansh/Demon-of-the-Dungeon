using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Player player;
    public float maxSight;
    Rigidbody2D rb;
    public float speed;
    public NavMeshAgent agent;
    Animator animator;
    public Camera cam;

    EnemyAttack attackScript;

    private void Start()
    {
        attackScript = GetComponent<EnemyAttack>();
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void FixedUpdate()
    {
        if (player.transform != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= maxSight)
            {
                FollowPlayer();

            }
            else
            {
                animator.SetBool("isRunning", false);
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
