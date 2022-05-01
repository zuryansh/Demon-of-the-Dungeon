using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Collider2D roomBounds;
    public int maxEnemyNum;
    public int currentEnemyNum;
    Room parentRoom;

    public List<GameObject> enemiesList = new List<GameObject>();
    
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
        for (int i = 0; i < maxEnemyNum; i++)
        {
            SpawnEnemy();
        }
    }

    private void OnDisable()
    {
        foreach (GameObject enemy in enemiesList)
        {
            Destroy(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (parentRoom.roomType == Room.RoomType.BossRoom && !once)
        {
            maxEnemyNum = 50;
            once = true;
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
        
        Vector2 position =  parentRoom.GetRandomPosInRoom();
        //Vector2 position = new Vector2(Random.Range(-11f, 10f), Random.Range(-10f, 2.7f));
        GameObject spawned = Instantiate(enemy, position, Quaternion.identity);
        spawned.GetComponent<enemy>().SetSpawnedFrom(gameObject);
        //spawned.transform.parent = null;
        enemiesList.Add(spawned);
        //Debug.Log("SPAWN");
    }


    
}
