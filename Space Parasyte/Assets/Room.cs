using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    CinemachineConfiner CMconfiner;

    public float roomHeight;
    public float roomWidth;
    public Spawner enemySpawner;
    public List<GameObject> Doors = new List<GameObject>();

    public GameObject indicator;
    public PolygonCollider2D confiner;
    RoomManager roomManager;

    public bool hasBeenBeaten = true;

    bool done = true;
    bool once = false;

    private void Start()
    {
        CMconfiner = FindObjectOfType<CinemachineConfiner>();

        roomManager = FindObjectOfType<RoomManager>();
        enemySpawner = GetComponentInChildren<Spawner>(true);

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

        if (enemySpawner != null)
        {   if (roomManager.GetCurrentRoom() == gameObject)
            {
                SetSpawnerState();
                CMconfiner.m_BoundingShape2D = confiner;
                SetRoomState();
            }
        }
            if (hasBeenBeaten)
            {
                SetDoorColliders(true);
            }
    }

    public void SetSpawnerState()
    {

        if (!enemySpawner.gameObject.activeInHierarchy)
        {
            enemySpawner.gameObject.SetActive(true);
        }
        else if (roomManager.GetCurrentRoom() != gameObject)
        {
            enemySpawner.gameObject.SetActive(false);
        }
    }

    public void SetRoomState()
    {
        if (enemySpawner.enemiesList.Count <= 0)
        {
            //all enemies have been defeated
            hasBeenBeaten = true;
        }
    }

    IEnumerator GetDoorColliders()
    {
        done = false;


        if (roomManager.GetCurrentRoom() == gameObject && hasBeenBeaten)
        {
            // it is current room

            if(roomManager.rooms[0] == this && !once)
            {
                //it is starting room
                once = true;
                yield return new WaitForSeconds(1f);
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
}
