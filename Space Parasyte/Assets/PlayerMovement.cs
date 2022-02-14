using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    public float speed;
    public Collider2D attackCollider;
    public Animator Animator;



    public int coins = 0;
    public bool isAttaclng;
    public int health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackCollider.enabled = false;
        
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement = new Vector2(x, y).normalized * speed * 10;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }

        #region Animator Setting
        if (rb.velocity.sqrMagnitude > 0.2) 
        {
            Animator.SetBool("isRunning", true);
        }
        else
        {
            Animator.SetBool("isRunning", false);
        }
            
        #endregion
    }

    private void FixedUpdate()
    {

        rb.velocity = movement * Time.fixedDeltaTime;
        
        //if (rb.velocity.sqrMagnitude > 0.1)
        //{
        //    Vector2 dir = rb.velocity;
        //    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}
    }

    public IEnumerator Attack()
    {

        Animator.SetTrigger("Attack");
        isAttaclng = true;

        yield return new WaitForSeconds(0.2f);

        isAttaclng = false;
    }


    public void AwardCoins(int coinsNum)
    {
        coins += coinsNum;
    }

    

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Die
        }
    }


}
