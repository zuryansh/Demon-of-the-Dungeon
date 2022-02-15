using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public int attackDamage = 5;
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            if (!collision.GetComponent<enemy>().isAttacked)
            {
                StartCoroutine(collision.GetComponent<enemy>().GotHit(attackDamage));
                Vector3 position = collision.transform.position;
                GameObject blood = Instantiate(Utilities.instance.blood, position, Quaternion.Euler(0, 0, Random.Range(1, 360)));
                Destroy(blood, 0.5f);
            }
        }
    }
}
