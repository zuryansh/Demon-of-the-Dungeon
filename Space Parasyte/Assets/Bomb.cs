using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int damage;
    public float detonationTime;
    CircleCollider2D collider2D;
    public GameObject explosionParticles;

    List<Collider2D> results = new List<Collider2D>();

    private void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
        Invoke("Detonate", detonationTime);
    }

    public void Detonate()
    {
        ContactFilter2D filter = new ContactFilter2D();


        //overlap sphere
        collider2D.OverlapCollider(filter, results);
        foreach (Collider2D hit in results)
        {
            if (hit.transform.CompareTag("Player"))
            {
                StartCoroutine(hit.GetComponent<Player>().TakeDamage(damage));
            }
        }
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        CinemachineShake.Instance.ShakeCamera(10f, 1.5f);
        Destroy(gameObject);


    }
}
