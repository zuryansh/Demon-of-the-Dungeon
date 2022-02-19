using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int health;
    public int attackDamage;

    float timeSinceChange;
    public bool canBeDamaged = true;
    public int coins = 0;
    public Slider healthSlider;

    public float invincibilityTime;
    public TextMeshProUGUI coinsText;
    public GameObject deathScreen;
    Rigidbody2D rb;


    private void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        rb = GetComponent<Rigidbody2D>();
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
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        rb.velocity = Vector2.zero;
    }

}
