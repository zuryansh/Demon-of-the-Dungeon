using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject enemy;
    public Collider2D roomBounds;
    public int maxEnemyNum;
    public int currentEnemyNum;
    public float spawnTime;

    public List<GameObject> enemiesList = new List<GameObject>();

    public bool isSearching;
    public float deadRadius;
    PlayerMovement player;

    float startSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        //startSpawnTime = spawnTime;
        roomBounds = GetComponent<Collider2D>();
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
        currentEnemyNum = enemiesList.Count;

        if (currentEnemyNum < maxEnemyNum)
        {

            SpawnEnemy();

        }
        yield return new WaitForSeconds(spawnTime);
        isSearching = false;
    }

    void SpawnEnemy()
    {

        Vector2 position = GetRandomPosition();
        //Vector2 position = new Vector2(Random.Range(-11f, 10f), Random.Range(-10f, 2.7f));
        GameObject spawned = Instantiate(enemy, position, Quaternion.identity);
        spawned.GetComponent<enemy>().SetSpawnedFrom(gameObject);
        enemiesList.Add(spawned);
        //Debug.Log("SPAWN");
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(roomBounds.bounds.min.x, roomBounds.bounds.max.x);
        float y = Random.Range(roomBounds.bounds.min.y, roomBounds.bounds.max.y);

        Vector2 position = new Vector2(x, y);
        if (Vector2.Distance(position, player.transform.position) < deadRadius)
        {
            //too close to player
            GetRandomPosition();
        }
        return position;
    }

}
