using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class weapon : MonoBehaviour
{
    public int attackDamage = 5;
    public int critChance;
    public BoxCollider2D colldier;
    PlayerAttack attackScript;

    private void Start()
    {
        attackScript = transform.root.GetComponent<PlayerAttack>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            if (!collision.GetComponent<enemy>().isAttacked)
            {
                    int temp = attackDamage;
                if (Random.Range(0, critChance) <= 1)
                {
                    attackDamage += 2;
                    attackScript.powerCharge += 2;

                }

                StartCoroutine(collision.GetComponent<enemy>().GotHit(attackDamage));
                Vector3 position = collision.transform.position;
                GameObject blood = Instantiate(Utilities.instance.blood, position, Quaternion.Euler(0, 0, Random.Range(1, 360)));
                attackDamage = temp;

               
                Destroy(blood, 0.5f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(colldier.bounds.center, colldier.bounds.size);
    }
}
