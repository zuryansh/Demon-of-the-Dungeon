using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMover : MonoBehaviour
{
    public Vector2 maxPos;
    public Vector2 minPos;
    public Vector3 randomPos;

    private void Start()
    {
        randomPos = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), 0);
        
    }

    private void Update()
    {


        MoveToRandomPos();
    }

    void MoveToRandomPos()
    {
        LeanTween.move(gameObject, randomPos, 10f);
        
        
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawCube(randomPos, new Vector3(10, 10, 1));
    //}
}
