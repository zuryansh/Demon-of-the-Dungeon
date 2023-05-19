using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPositioner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Utilities.instance.player.transform.position.x, Utilities.instance.player.transform.position.y, transform.position.z);
    }
}
