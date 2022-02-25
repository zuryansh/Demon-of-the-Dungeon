using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float roomHeight;
    public float roomWidth;
    public GameObject[] roomSpawners;


    private void Start()
    {
        FindObjectOfType<RoomManager>().rooms.Add(gameObject);
    }

}
