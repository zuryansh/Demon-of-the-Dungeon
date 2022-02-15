using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    public int maxEnemyNum;
    public int currentEnemyNum;
    public float spawnTime;
    bool isSearching;
    public float deadRadius;
    PlayerMovement player;

    float startSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        startSpawnTime = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isSearching)
        {
            StartCoroutine(GetEnemyCount());
        }
    }

    IEnumerator GetEnemyCount()
    {
        isSearching = true;
        currentEnemyNum = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        if (currentEnemyNum < maxEnemyNum)
        {
            if(currentEnemyNum < maxEnemyNum / 2 && spawnTime>0.5f)
            {
                spawnTime = 0.5f;
            }
            else if(currentEnemyNum > maxEnemyNum / 2 && spawnTime == 0.5f)
            {
                spawnTime = startSpawnTime;
            }

            SpawnEnemy();
            
        }
        yield return new WaitForSeconds(spawnTime);
        isSearching = false;
    }

    void SpawnEnemy()
    {
        
        Vector2 position = GetRandomPosition();
        //Vector2 position = new Vector2(Random.Range(-11f, 10f), Random.Range(-10f, 2.7f));
        Instantiate(enemy, position , Quaternion.identity);
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(-11f, 10f);
        float y = Random.Range(-10f, 2.7f);
        Vector2 position = new Vector2(x, y);
        if(Vector2.Distance(position , player.transform.position) < deadRadius)
        {
            //too close to player
            GetRandomPosition();
        }
        return position;
    }
    
}
