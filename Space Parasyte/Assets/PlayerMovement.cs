using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    bool canBeDamaged = true;

    public float speed;

    public Animator Animator;


    public Slider healthSlider;
    public int coins = 0;
    public bool isAttaclng;
    public int health;
    public float invincibilityTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        healthSlider.maxValue = health;
        healthSlider.value = health;

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

    

    public IEnumerator TakeDamage(int damage)
    {
        if (canBeDamaged)
        {
            canBeDamaged = false;

            health -= damage;
            healthSlider.value = health;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(invincibilityTime);
            canBeDamaged = true;
        }
    }


}
