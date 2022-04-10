using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    GameObject spawnedFrom;
    public Player player;

    [HideInInspector]public Animator animator;


    public bool isAttacked;

    public int health = 10;
    public int rewardedCoins = 5;
    //public int damage = 5;

    //public Camera cam;
    public float attackTime;
    public Slider healthSlider;
    public GameObject[] loot;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        healthSlider.maxValue = health;
        healthSlider.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (healthSlider.gameObject.activeInHierarchy== false) healthSlider.gameObject.SetActive(true);
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        player.AwardCoins(rewardedCoins);
        if (spawnedFrom.GetComponent<Spawner>() != null)
        {
            spawnedFrom.GetComponent<Spawner>().enemiesList.Remove(gameObject);
        }
        else
        {
            spawnedFrom.GetComponent<BossSpawner>().enemiesList.Remove(gameObject);
        }
        //spawn loot
        if (Random.Range(0, 3) < 1)
        {
            GameObject drop = loot[Random.Range(0, loot.Length)];
            Instantiate(drop, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

   

    public IEnumerator GotHit(int damage)
    {

        isAttacked = true;
        Utilities.instance.EnemyHit();
        CinemachineShake.Instance.ShakeCamera(0.7f, 0.1f);

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        isAttacked = false;
        TakeDamage(damage);
    }

    public void SetSpawnedFrom(GameObject spawner)
    {
        spawnedFrom = spawner;
    }

}
