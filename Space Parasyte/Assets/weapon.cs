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
                StartCoroutine(collision.GetComponent<enemy>().GotHit(attackDamage));
        }
    }
}
