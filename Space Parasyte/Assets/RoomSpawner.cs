using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
     GameObject room;
    Room parentRoom;
    RoomManager roomManager;

    RaycastHit2D[] results = new RaycastHit2D[10];

    public enum SpawnSide
    {
        Top,
        Bottom,
        Right,
        Left
    }
    public SpawnSide spawnSide;

    // Start is called before the first frame update
    void Start()
    {
        
        roomManager = FindObjectOfType<RoomManager>();
        parentRoom = GetComponentInParent<Room>();
        

        Invoke("CorrectPosition", 0.1f);
        

        if (roomManager.currentRooms <= roomManager.maxRooms)
        {
            Invoke("SpawnRoom", 0.2f);
        }
    }

    public void CorrectPosition()
    {
        

        
        if (spawnSide == SpawnSide.Top)
        {
            transform.localPosition += new Vector3(0, parentRoom.roomHeight/2,0);
        }
        else if (spawnSide == SpawnSide.Bottom)
        {
            transform.localPosition -= new Vector3(0, parentRoom.roomHeight/2,0);
        }
        else if (spawnSide == SpawnSide.Right)
        {
            transform.localPosition += new Vector3(parentRoom.roomWidth/2 ,0,0);
        }
        else if (spawnSide == SpawnSide.Left)
        {
            transform.localPosition -= new Vector3(parentRoom.roomWidth/2 ,0,0);
        }
    }

   

    public void SpawnRoom()
    {
       
        roomManager.currentRooms++;
        if (spawnSide == SpawnSide.Top)
        {
            room = roomManager.BottomRooms[Random.Range(0, roomManager.BottomRooms.Length)];
        }
        else if (spawnSide == SpawnSide.Bottom)
        {
            room = roomManager.TopRooms[Random.Range(0, roomManager.BottomRooms.Length)];

        }
        else if (spawnSide == SpawnSide.Right)
        {
            room = roomManager.LeftRooms[Random.Range(0, roomManager.BottomRooms.Length)];

        }
        else if (spawnSide == SpawnSide.Left)
        {
            room = roomManager.RightRooms[Random.Range(0, roomManager.BottomRooms.Length)];

        }

        Instantiate(room, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }


}
