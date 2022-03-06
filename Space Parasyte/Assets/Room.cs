using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float roomHeight;
    public float roomWidth;
    public List<GameObject> Doors = new List<GameObject>();

    public GameObject indicator;
    public PolygonCollider2D confiner;
    RoomManager roomManager;

    bool done = true;
    bool once = false;

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();

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

            StartCoroutine(SetDoorColliders());
        }
    }

    IEnumerator SetDoorColliders()
    {
        done = false;


        if (roomManager.GetCurrentRoom() == gameObject)
        {
            if(roomManager.rooms[0] == this && !once)
            {
                //it is starting room
                once = true;
                yield return new WaitForSeconds(1f);
            }

            // set colliders to trigger
            foreach (GameObject door in Doors)
            {
                door.GetComponent<Collider2D>().isTrigger = true;
            }
        }
        else
        {
            // set colliders to trigger
            foreach (GameObject door in Doors)
            {
                door.GetComponent<Collider2D>().isTrigger = false;
            }
        }
        done = true;
    }
}
