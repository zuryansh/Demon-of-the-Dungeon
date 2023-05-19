using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Collider2D roomBounds;
    [SerializeField]int budget = 10;
    public int maxEnemyNum;
    public int currentEnemyNum;
    Room parentRoom;

    public GameObject[] enemies;
    public List<GameObject> spawnedEnemiesList = new List<GameObject>();
    
    bool isSearching;
    public float deadRadius;
    PlayerMovement player;
    bool once;
    float startSpawnTime;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        //startSpawnTime = spawnTime;
        roomBounds = GetComponent<Collider2D>();
        parentRoom = transform.root.GetComponent<Room>();

        budget = PlayerPrefs.GetInt("FloorNo",1) * 15;


    }


    private void OnEnable()
    {
            SpawnEnemy();
    }

    private void OnDisable()
    {
        foreach (GameObject enemy in spawnedEnemiesList)
        {
            Destroy(enemy);
        }
    }

    void SpawnEnemy()
    {
        if (budget != 0)
        {

            Vector2 position = parentRoom.GetRandomPosInRoom();

            GameObject spawned = Instantiate(GetRandomEnemy(), position, Quaternion.identity);
            spawned.GetComponent<enemy>().SetSpawnedFrom(gameObject);

            spawnedEnemiesList.Add(spawned);

            SpawnEnemy();
        }
    }

    GameObject GetRandomEnemy()
    {
        enemy enemy = enemies[Random.Range(0, enemies.Length)].GetComponent<enemy>();
        
        if ((budget - enemy.SO.spawnerValue < 0))
        {
            //we cant afford it
            return GetRandomEnemy().gameObject; // try again
        }
        // we can afford it
        budget -= enemy.SO.spawnerValue; // subtract the cost from the total
        return enemy.gameObject;
    }
    
}
