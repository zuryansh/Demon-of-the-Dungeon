using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    GameObject room;
    Room parentRoom;
    RoomManager roomManager;
    Collider2D myCollider;
    public LayerMask layerMask;
    public GameObject leadsToRoom;
    public bool canSetRoom;
    bool once;
    public bool ENTRYROOM;

    List<RaycastHit2D> results = new List<RaycastHit2D>();
    ContactFilter2D contactFilter = new ContactFilter2D();

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
        myCollider = GetComponent<Collider2D>();
        contactFilter.SetLayerMask(layerMask);




        if (roomManager.rooms.Count <= roomManager.maxRooms)
        {
            SpawnRoom();
        }
    }

    private void Update()
    {
        if(roomManager.GetCurrentRoom() == transform.root.gameObject)
        {
            canSetRoom = ShouldSetRoom();
        }
        else { canSetRoom = false; }



    }

    Vector2 GetSpawnPosition()
    {

        Vector2 spawnPosition = transform.position;

        if (spawnSide == SpawnSide.Top)
        {
            spawnPosition = transform.position + new Vector3(0, parentRoom.roomHeight / 2, 0);
        }
        else if (spawnSide == SpawnSide.Bottom)
        {
            spawnPosition = transform.position - new Vector3(0, parentRoom.roomHeight / 2, 0);
        }
        else if (spawnSide == SpawnSide.Right)
        {
            spawnPosition = transform.position + new Vector3(parentRoom.roomWidth / 2, 0, 0);
        }
        else if (spawnSide == SpawnSide.Left)
        {
            spawnPosition = transform.position - new Vector3(parentRoom.roomWidth / 2, 0, 0);
        }

        return spawnPosition;
    }

    void CastColliderFromDoor()
    {
        //results.Clear();

        if (spawnSide == SpawnSide.Top || spawnSide == SpawnSide.Bottom)
        {
            myCollider.Cast(transform.up, contactFilter, results, parentRoom.roomHeight / 2);
        }
        else if (spawnSide == SpawnSide.Right || spawnSide == SpawnSide.Left)
        {

            myCollider.Cast(transform.up, contactFilter, results, parentRoom.roomWidth / 2);
        }

    }

    bool CheckValidSpawn()
    {

        CastColliderFromDoor();

        return !(results.Count > 0);


    }

    public void SpawnRoom()
    {
        //yield return new WaitForSeconds(0f);

        #region Room Selection
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
        #endregion

        if (CheckValidSpawn())
        {
            leadsToRoom = Instantiate(room, GetSpawnPosition(), Quaternion.identity);
            
            leadsToRoom.transform.name = "Room" + roomManager.rooms.Count;
        }



    }

    void SetRoom()
    {
        
        if (leadsToRoom != null)
        {
            roomManager.SetCurrentRoom(leadsToRoom);
        }
        else
        {
            roomManager.SetCurrentRoom(transform.root.gameObject);
        }

    }

    public void SetLeadingRoom()
    {

        CastColliderFromDoor();
        //Debug.Log(results.Count , gameObject);
        
        if (results.Count > 0)
        {
            leadsToRoom = results[0].transform.root.gameObject;
        }
        else if (results.Count <= 0)
        {
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && canSetRoom)
        {
            Invoke("SetRoom", 0.2f);
        }

    }

    bool ShouldSetRoom()
    {
        Vector2 playerDir = Utilities.playerMovement.GetPlayerDirection();
        //Debug.Log(playerDir); 

        if(spawnSide == SpawnSide.Top && playerDir.y > 0)
        {// moving up
            
            return true;
        }
        else if (spawnSide == SpawnSide.Bottom && playerDir.y < 0)
        {//movig down
            return true;
        }

        else if (spawnSide == SpawnSide.Right && playerDir.x > 0)
        {// moving right
            return true;
        }
        else if (spawnSide == SpawnSide.Left && playerDir.x < 0)
        {//moving left
            return true;
        }
        

        return false;
    }

}
