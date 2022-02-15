using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool isAttacked;
    public bool isAttacking;
    PlayerMovement player;
    public float maxSight;
    Rigidbody2D rb;
    public float speed;
    NavMeshAgent agent;
    Animator animator;
    public float attackForce;

    public int health = 10;
    public int rewardedCoins = 5;
    public int damage = 5;

    public Camera cam;
    public float attackTime;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        player.AwardCoins(rewardedCoins);
        Destroy(gameObject);
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
        CheckForAttack();

    }

    void CheckForAttack()
    {
        if(Vector2.Distance(player.transform.position , transform.position)<=agent.stoppingDistance)
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
        StartCoroutine(player.TakeDamage(damage));
        yield return new WaitForSeconds(1f);
        isAttacking = false;

    }

    public IEnumerator GotHit(int damage)
    {
        
        isAttacked = true;
        Utilities.instance.EnemyHit();
        CinemachineShake.Instance.ShakeCamera(2.5f, 0.1f);

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        isAttacked = false;
        TakeDamage(damage);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        collision.transform.GetComponent<PlayerMovement>().TakeDamage(damage);
    //    }
    //}

}
