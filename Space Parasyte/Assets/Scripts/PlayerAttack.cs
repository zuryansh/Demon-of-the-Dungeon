using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown;
    Animator animatior;
    public bool isAttackng;


    private void Start()
    {
        animatior = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {

        animatior.SetTrigger("Attack");
        isAttackng = true;

        yield return new WaitForSeconds(0.2f);

        isAttackng = false;
    }
}
