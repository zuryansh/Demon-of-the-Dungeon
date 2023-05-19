using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public int floorNo;

    //    public void SetFloorNo(int val)
    //    {
    //        floorNo = val;
    //    }

    private void Start()
    {
        PlayerPrefs.SetInt("FloorNo", 1);
    }
    //private void Update()
    //{
    //    PlayerPrefs.SetInt("FloorNo", floorNo);
    //}
}
