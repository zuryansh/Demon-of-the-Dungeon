using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class enemy : MonoBehaviour
{
    public EnemySO SO;
    SpriteRenderer spriteRenderer;
    GameObject spawnedFrom;
    public Player player;

    List<GameObject> trails = new List<GameObject>();

    [HideInInspector]public Animator animator;


    public bool isAttacked;

    public int health; 
    public Slider healthSlider;
    public int spawnerValue;
    public BoxCollider2D colldier;

    public GameObject GetSpawnedFrom()
    {
        return spawnedFrom;
        
    }

    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        colldier = GetComponent<BoxCollider2D>();

        health = SO.health;
        healthSlider.maxValue = health;
        healthSlider.gameObject.SetActive(false);


        animator.runtimeAnimatorController = SO.animator as RuntimeAnimatorController;
        spriteRenderer.color = SO.spriteColor;
    }

    private void Start()
    {

        if (SO.leavesPoisonTrail)
        {
            StartCoroutine(LeavePoisonTrail());
        }
    }

    IEnumerator LeavePoisonTrail()
    {
        GameObject obj = Instantiate(SO.poisonTrail, transform.position, Quaternion.identity);
        trails.Add(obj);
        StartCoroutine(obj.GetComponent<PoisonPuddle>().DestroyPuddle(SO.trailDuration));
        yield return new WaitForSeconds(SO.trailcooldown);
        StartCoroutine(LeavePoisonTrail());
        
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
        player.AwardCoins(SO.rewardedCoins);
        if (spawnedFrom.GetComponent<Spawner>() != null)
        {
            spawnedFrom.GetComponent<Spawner>().spawnedEnemiesList.Remove(gameObject);
        }

        //spawn loot
        if (Random.Range(0, 3) < 1)
        {
            
           
            GameObject loot = Instantiate(GetItemDrop(), transform.position, Quaternion.identity) ;
            loot.transform.parent = spawnedFrom.transform.root;
            
        }

        foreach (GameObject puddle in trails)
        {
            Destroy(puddle);
        }

        Destroy(gameObject);
    }

   
    GameObject GetItemDrop()
    {
        GameObject drop = SO.loot[Random.Range(0, SO.loot.Length)];
        if (PlayerPrefs.GetInt("FloorNo")> 2 && drop.GetComponent<Pickup>().pickupType == Pickup.Type.HealthPotion)
        {
            Debug.Log("NOT ALLOWED");
            // we are not allowd to drop health and it is health
            drop = GetItemDrop();
        }
        return drop;
    }

    public IEnumerator GotHit(int damage)
    {

        if (!isAttacked)
        {
            Color defaultColor = spriteRenderer.color;

            isAttacked = true;
            Utilities.instance.EnemyHit();
            CinemachineShake.Instance.ShakeCamera(0.7f, 0.1f);

            Vector3 pos = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), 0);

            GameObject text = Instantiate(Utilities.instance.PopupText, transform.position + pos, Quaternion.identity);
            TextMeshProUGUI textComponent = text.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = damage.ToString();
            if (damage > 5)
            {
                textComponent.color = new Color(0.915f, 0.279f, 0.1647f);
            }
            Destroy(text, 1f);

            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = defaultColor;
            isAttacked = false;
            TakeDamage(damage);
        }
    }

    public void SetSpawnedFrom(GameObject spawner)
    {
        spawnedFrom = spawner;
    }

    private void OnDrawGizmos()
    {
        if (isAttacked)
        {
            Gizmos.color = Color.red;
        }
        else
        {

        Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(colldier.bounds.center, colldier.bounds.size);
    }

}
