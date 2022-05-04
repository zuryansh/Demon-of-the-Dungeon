using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Collider2D roomBounds;
    public int budget;
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
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        //startSpawnTime = spawnTime;
        roomBounds = GetComponent<Collider2D>();
        parentRoom = transform.root.GetComponent<Room>();

    }

    private void OnEnable()
    {
        //for (int i = 0; i < maxEnemyNum; i++)
        //{
        //    SpawnEnemy();
        //}
        SpawnEnemy();
    }

    private void OnDisable()
    {
        foreach (GameObject enemy in spawnedEnemiesList)
        {
            Destroy(enemy);
        }
    }




    //IEnumerator GetEnemyCount()
    //{
    //    isSearching = true;
    //    currentEnemyNum = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
    //    if (currentEnemyNum < maxEnemyNum)
    //    {
            
    //        SpawnEnemy();
            
    //    }
    //    yield return new WaitForSeconds(spawnTime);
    //    isSearching = false;
    //}

    void SpawnEnemy()
    {
        if (budget != 0)
        {

            Vector2 position = parentRoom.GetRandomPosInRoom();
            //Vector2 position = new Vector2(Random.Range(-11f, 10f), Random.Range(-10f, 2.7f));
            GameObject spawned = Instantiate(GetRandomEnemy(), position, Quaternion.identity);
            spawned.GetComponent<enemy>().SetSpawnedFrom(gameObject);
            //spawned.transform.parent = null;
            spawnedEnemiesList.Add(spawned);
            //Debug.Log("SPAWN");
            SpawnEnemy();
        }
    }

    GameObject GetRandomEnemy()
    {
        enemy enemy = enemies[Random.Range(0, enemies.Length)].GetComponent<enemy>();
        
        if ((budget - enemy.SO.spawnerValue < 0))
        {
            //we cant afford it
            Debug.Log( (enemy.SO.spawnerValue  - budget), gameObject);
            SpawnEnemy(); //try again
            return null;
        }
        // we can afford it
        budget -= enemy.SO.spawnerValue; // subtract the cost from the total
        Debug.Log("sucess", enemy.gameObject);
        return enemy.gameObject;
    }
    
}
