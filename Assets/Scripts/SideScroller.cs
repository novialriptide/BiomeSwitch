using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroller : MonoBehaviour
{
    public bool moving = false;
    public float xAxisSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x + xAxisSpeed * Time.deltaTime, pos.y, pos.z);
        }
    }
}
