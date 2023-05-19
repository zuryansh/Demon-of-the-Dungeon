using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum ProjectileType
    {
        Enemy,
        Player
    }

    public ProjectileType projectileType;
    public Vector2 velocity;
    Rigidbody2D rb;
    public int damage;
    public bool hasEffect;
    public GameObject particle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = velocity;

        Destroy(gameObject, 4f);

        Quaternion rotation = Quaternion.LookRotation(velocity);
        rotation.x = 0;
        rotation.y = 0;
        transform.rotation = rotation;
        //if (transform.rotation.z < 0)
        //{
        //    GetComponent<SpriteRenderer>().flipX = true;
        //}

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
            //Debug.Log("COLLISION");
        if (projectileType == ProjectileType.Enemy)
        {
            if (collision.transform.CompareTag("Player"))
            {
                StartCoroutine(Utilities.instance.player.GetComponent<Player>().TakeDamage(damage));

                SpawnParticles();
                Destroy(gameObject);
            }
        }
        else if(projectileType == ProjectileType.Player)
        {
            if (collision.CompareTag("Enemy"))
            {
                StartCoroutine(collision.GetComponent<enemy>().GotHit(damage));
                Vector3 position = collision.transform.position;
                GameObject blood = Instantiate(Utilities.instance.blood, position, Quaternion.Euler(0, 0, Random.Range(1, 360)));
                Destroy(blood, 0.5f);
                CinemachineShake.Instance.ShakeCamera(8f, 1f);

                SpawnParticles();

            }
        }
        

    }

    void SpawnParticles()
    {
        if (hasEffect)
        {
            Transform particles = Instantiate(particle, transform.position, Quaternion.identity).transform;
            particles.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

}
