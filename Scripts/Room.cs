using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    CinemachineConfiner CMconfiner;
    public enum RoomType
    {
        NormalRoom,
        BossRoom
    }
    public RoomType roomType;
    public float roomHeight;
    public float roomWidth;
    public Spawner enemySpawner;
    public List<GameObject> Doors = new List<GameObject>();
    public GameObject[] minimapIcon;
   

    public PolygonCollider2D confiner;
    RoomManager roomManager;

    public bool hasBeenBeaten = true;
    public List<Pickup> coinsList = new List<Pickup>();

    bool done = true;
    bool once = false;


    private void Start()
    {
        CMconfiner = FindObjectOfType<CinemachineConfiner>();

        roomManager = FindObjectOfType<RoomManager>();
        if (enemySpawner == null)
        {
            enemySpawner = GetComponentInChildren<Spawner>(true);
        }
        roomManager.rooms.Add(this);

        foreach (RoomSpawner door in GetComponentsInChildren<RoomSpawner>())
        {
            Doors.Add(door.gameObject);
        }
        
    }

    private void Update()
    {
        if (done && roomManager.hasFinishedSpawning)
        {
            StartCoroutine(GetDoorColliders());
        }

        if (roomManager.GetCurrentRoom() == gameObject ) 
        {
            
                //if (roomType == RoomType.NormalRoom)
                //{
                    if (enemySpawner != null)
                    {
                        
                        SetSpawnerState(enemySpawner.gameObject);
                        CMconfiner.m_BoundingShape2D = confiner;
                        SetRoomState();
                    }

                SetMinimapIcon(true);
     
        }
        else
        {
            SetMinimapIcon(hasBeenBeaten);
            
        }

        if (hasBeenBeaten)
        {
            SetDoorColliders(true); 
        }
        
        
    }

    void SetMinimapIcon(bool val)
    {
        
        foreach (GameObject icon in minimapIcon)
        {
            icon.SetActive(val);
        }
    }

    public void SetSpawnerState(GameObject spawner)
    {

        if (!spawner.activeInHierarchy)
        {
            spawner.SetActive(true);
        }
        else if (roomManager.GetCurrentRoom() != gameObject)
        {
            spawner.SetActive(false);
        }
    }

    public void SetRoomState()
    {
        //if (roomType == RoomType.NormalRoom)
        //{
            if (enemySpawner.spawnedEnemiesList.Count <= 0)
            {
                //all enemies have been defeated
                hasBeenBeaten = true;
                // add to the defeated rooms list
                if (!roomManager.defeatedRooms.Contains(gameObject))
                {
                    roomManager.defeatedRooms.Add(gameObject);
                }
            foreach (Pickup coin in coinsList)
            {
                if (coin.pickupType != Pickup.Type.HealthPotion)
                {
                    coin.transform.position = Utilities.instance.player.transform.position;
                }
            }
            }
        //}
        
    }

    IEnumerator GetDoorColliders()
    {
        done = false;


        if (roomManager.GetCurrentRoom() == gameObject && hasBeenBeaten)
        {
            // it is current room

            if((roomManager.rooms[0] == this && !once))
            {
                //it is starting room
                once = true;
                yield return new WaitForSeconds(5f);

            }
            // set colliders to trigger
                SetDoorColliders(true);
        }
        else
        {
            SetDoorColliders(false);
        }
        done = true;
    }

    public void SetDoorColliders(bool val) {

            foreach (GameObject door in Doors)
        {
            //yield return new WaitForSeconds(0.5f);
            door.GetComponent<Collider2D>().isTrigger = val;
        }
    }

    public Vector2 GetRandomPosInRoom()
    {

        float x = Random.Range(enemySpawner.roomBounds.bounds.min.x, enemySpawner.roomBounds.bounds.max.x);
        float y = Random.Range(enemySpawner.roomBounds.bounds.min.y, enemySpawner.roomBounds.bounds.max.y);

        Vector2 position = new Vector2(x, y);
        if (Vector2.Distance(position, FindObjectOfType<Player>().transform.position) < 5)
        {
            //too close to player
            GetRandomPosInRoom();
        }
        return position;

        
    }

    
   

}
