using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

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
    public bool hasFinishedSpawning;



    private void Start()
    {


        if (FindObjectOfType<Room>() == null)
        {
            Debug.LogError("NO ENTRY ROOM FOUND");
        }
        else
        {
            GameObject room = FindObjectOfType<Room>().gameObject;
            
            SetCurrentRoom(room);
        }
    }

    private void Update()
    {
        //Debug.Log(currentRoom, currentRoom);
        

        if (rooms.Count > maxRooms && !hasFinishedSpawning)
        {
            StartCoroutine(FinishedSpawning());
        }

        
    }

    public GameObject GetCurrentRoom()
    {
        return currentRoom;
    }

    public IEnumerator FinishedSpawning()
    {
        hasFinishedSpawning = true;
        yield return new WaitForSeconds(0.7f);

        foreach (Room room in rooms)
        {
            foreach (GameObject door in room.Doors)
            {

                door.GetComponent<RoomSpawner>().SetLeadingRoom();
                
                //door.GetComponent<Collider2D>().isTrigger = true;
            }
        }


    }

    public void SetCurrentRoom(GameObject room)
    {
        if (currentRoom != null)
        {
            currentRoom.GetComponentInChildren<Tilemap>().color = Color.white;
        }
        //Debug.Log(room.name);
        currentRoom = room;
        
        currentRoom.GetComponentInChildren<Tilemap>().color = Color.green;
    }


    public void ResetLevels()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
