using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    public bool canBeDamaged = true;

    public float speed;

    public Animator Animator;


    public Slider healthSlider;
    public int coins = 0;
    public bool isAttaclng;
    public int health;
    public float invincibilityTime;

    public TextMeshProUGUI coinsText;
    public GameObject deathScreen;
    float timeSinceChange;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        healthSlider.maxValue = health;
        healthSlider.value = health;

    }

    private void Update()
    {

        coinsText.text = coins.ToString();
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement = new Vector2(x, y).normalized * speed * 10;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }

    
        if ( Time.realtimeSinceStartup - timeSinceChange > 3)
        {
            canBeDamaged = true;
            timeSinceChange = Time.realtimeSinceStartup;
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
                Die();
            }
            yield return new WaitForSeconds(invincibilityTime);
            canBeDamaged = true;
            timeSinceChange = Time.realtimeSinceStartup;

        }
    }

    public void Die()
    {
        coins = 0;
        canBeDamaged = true;
        deathScreen.SetActive(true);
        enabled = false;
    }
}
