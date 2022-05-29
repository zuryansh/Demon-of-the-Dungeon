using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 velocity;
    Rigidbody2D rb;
    public int damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartCoroutine(Utilities.instance.player.GetComponent<Player>().TakeDamage(damage));
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5f);
        }
    }

}
