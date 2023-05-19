using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int damage;
    public float detonationTime;
    CircleCollider2D myCollider;
    public GameObject explosionParticles;

    List<Collider2D> results = new List<Collider2D>();

    private void Start()
    {
        myCollider = GetComponent<CircleCollider2D>();
        Invoke("Detonate", detonationTime);
    }

    public void Detonate()
    {
        ContactFilter2D filter = new ContactFilter2D();


        //overlap sphere
        myCollider.OverlapCollider(filter, results);
        foreach (Collider2D hit in results)
        {
            if (hit.transform.CompareTag("Player"))
            {
                StartCoroutine(hit.GetComponent<Player>().TakeDamage(damage));
            }
        }
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        CinemachineShake.Instance.ShakeCamera(8f, 1f);
        Destroy(gameObject);


    }
}
