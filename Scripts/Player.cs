using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int attackDamage;

    float timeSinceChange;
    public bool canBeDamaged = true;
    public int coins = 0;
    public Slider healthSlider;


    public float invincibilityTime;
    public TextMeshProUGUI coinsText;
    public GameObject deathScreen;
    Rigidbody2D rb;
    AudioSource playerHit;

    Animator animator;
    public BoxCollider2D playerCollider;


    private void Start()
    {
        health = PlayerPrefs.GetInt("PlayerHealth");
        //Debug.Log(GameManager.instance.playerHealth + " " + health);
        rb = GetComponent<Rigidbody2D>();
        playerHit = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    

    private void Update()
    {
        
        coinsText.text = coins.ToString();

        if (Time.realtimeSinceStartup - timeSinceChange > 3)
        {
            canBeDamaged = true;
            timeSinceChange = Time.realtimeSinceStartup;
        }
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

            //Effects
            CinemachineShake.Instance.ShakeCamera(4f, 0.3f);
            animator.SetTrigger("Hit");
            playerHit.Play();
            

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
        PlayerPrefs.SetInt("PlayerHealth", maxHealth);
        coins = 0;
        canBeDamaged = true;
        deathScreen.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        rb.velocity = Vector2.zero;
    }

    public void AddHealth(int health)
    {

        this.health += health; // add health to current health
        if (this.health > maxHealth) // limit our health to the max health
        {
            this.health = maxHealth;
        }
        healthSlider.value = this.health;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playerCollider.bounds.center, playerCollider.bounds.size);
        

    }
}
