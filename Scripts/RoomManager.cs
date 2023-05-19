using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using TMPro;


public class RoomManager : MonoBehaviour
{
    

    public int maxRooms;
    

    public List<Room> rooms = new List<Room>();
    public GameObject[] TopRooms;
    public GameObject[] BottomRooms;
    public GameObject[] RightRooms;
    public GameObject[] LeftRooms;

    public List<GameObject> defeatedRooms = new List<GameObject>();
    public GameObject entryRoom;


    [SerializeField]GameObject currentRoom;
    [SerializeField] Room bossRoom;
    public bool hasFinishedSpawning;
    public GameObject BossRoomPrefab;
    
    public TextMeshProUGUI roomCounter;
    bool once;

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


    private void Update()
    {
        roomCounter.text = (rooms.Count - defeatedRooms.Count).ToString();

        if (bossRoom != null)
        {
            if (bossRoom.hasBeenBeaten && !once)
            {
                // we beat the game
                once = true;
                StartCoroutine(GameManager.instance.LevelWon());
            }
        }
    }

    void GetBossRoom()
    {
        

        bossRoom = rooms[rooms.Count - 1];
        bossRoom.roomType = Room.RoomType.BossRoom;
        //Vector3 newPosition = bossRoom.transform.position;
        //Destroy(bossRoom);
        //bossRoom =Instantiate(BossRoomPrefab, newPosition, Quaternion.identity);
        //bossRoom.enemySpawner.budget += 30;
        Instantiate(Utilities.instance.BossSkull, bossRoom.transform.position, Quaternion.identity);

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
