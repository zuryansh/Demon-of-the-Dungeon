using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int maxRooms;
    public int currentRooms;

    public List<GameObject> rooms = new List<GameObject>();
    public GameObject[] TopRooms;
    public GameObject[] BottomRooms;
    public GameObject[] RightRooms;
    public GameObject[] LeftRooms;
    public GameObject entryRoom;



    public void ResetLevels()
    {
        foreach (GameObject room  in rooms)
        {
            Destroy(room);
        }
            currentRooms = 0;
        rooms.Clear();
            Instantiate(entryRoom, Vector3.zero, Quaternion.identity);
    }
}
