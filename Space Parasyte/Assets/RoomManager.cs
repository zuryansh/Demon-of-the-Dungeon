using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.AI;


public class RoomManager : MonoBehaviour
{
    

    public int maxRooms;
    public int currentRooms;

    public List<Room> rooms = new List<Room>();
    public GameObject[] TopRooms;
    public GameObject[] BottomRooms;
    public GameObject[] RightRooms;
    public GameObject[] LeftRooms;
    public GameObject entryRoom;


    [SerializeField]GameObject currentRoom;
    [SerializeField] GameObject bossRoom;
    public bool hasFinishedSpawning;
    public GameObject BossRoomPrefab;
    public NavMeshSurface2d surface;


    private void Awake()
    {


        if (FindObjectOfType<Room>() == null)
        {
            Debug.LogError("NO ENTRY ROOM FOUND");
        }
        else
        {
            GameObject room = FindObjectOfType<Room>().gameObject;
            
            SetCurrentRoom(room);
            
                StartCoroutine(FinishedSpawning());
            
        }
        Invoke("GetBossRoom", 3f);
    }


    void GetBossRoom()
    {
        bossRoom = rooms[rooms.Count - 1].gameObject;
        Vector3 newPosition = bossRoom.transform.position;
        Destroy(bossRoom);
        bossRoom =Instantiate(BossRoomPrefab, newPosition, Quaternion.identity);
        bossRoom.GetComponentInChildren<Tilemap>().color = Color.blue;
        
    }

    public GameObject GetCurrentRoom()
    {
        return currentRoom;
    }

    public IEnumerator FinishedSpawning()
    {
        //Wait for everyting to finish spawning
        yield return new WaitForSeconds(4f);
        hasFinishedSpawning = true;

        foreach (Room room in rooms)
        {
            if (room.roomType != Room.RoomType.BossRoom)
            {
                foreach (GameObject door in room.Doors)
                {
                    //disable all the one sided colliders
                    //door.GetComponent<PlatformEffector2D>().enabled = false;
                    //tell every single door to set its leading room
                    if (door.GetComponent<RoomSpawner>() != null)
                    door.GetComponent<RoomSpawner>().SetLeadingRoom();
                    // renable one sided colliders


                    //door.GetComponent<Collider2D>().isTrigger = true;
                }
            }
        }

        //surface.BuildNavMesh();

    }

    public void SetCurrentRoom(GameObject room)
    {
        if (currentRoom != null)
        {
            currentRoom.GetComponentInChildren<Tilemap>().color = Color.white;
        }
        //Debug.Log(room.name);
        currentRoom = room;
        
        currentRoom.GetComponentInChildren<Tilemap>().color = Color.white;
    }


    public void ResetLevels()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
