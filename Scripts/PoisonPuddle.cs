using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPuddle : MonoBehaviour
{
    public int damage;
    public float cooldown;
    public bool hasContact;
    public bool isDamaging;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            hasContact = true;
        }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            hasContact = false;
        }
    }

    private void Update()
    {
        if (!isDamaging)
        {
            StartCoroutine(DamagePlayer());
        }
    }

    IEnumerator DamagePlayer()
    {
        if (hasContact)
        {
            isDamaging = true;
            StartCoroutine(Utilities.instance.player.TakeDamage(damage));
            yield return new WaitForSeconds(cooldown);
            isDamaging = false;
        }
    }

    public IEnumerator DestroyPuddle(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetTrigger("Fade");


    }
    void Destroy_()
    {
        Destroy(gameObject);
    }
}
