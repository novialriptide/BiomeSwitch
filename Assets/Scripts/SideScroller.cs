using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroller : MonoBehaviour
{
    public float xAxisSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x + xAxisSpeed * Time.deltaTime, pos.y, pos.z);
    }
}
