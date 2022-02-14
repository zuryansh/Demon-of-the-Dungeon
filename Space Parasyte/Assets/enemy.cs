using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool isAttacked;
    PlayerMovement player;
    public float maxSight;
    Rigidbody2D rb;
    public float speed;
    NavMeshAgent agent;

    public int health = 10;
    public int rewardedCoins = 5;

    public Camera cam;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();

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
        if (Vector2.Distance(transform.position, player.transform.position) <= maxSight )
        {
            agent.SetDestination(player.transform.position);

        }

    }
   
    public IEnumerator GotHit(int damage)
    {
        
        isAttacked = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.green;
        isAttacked = false;
        TakeDamage(damage);
    }

  
}
