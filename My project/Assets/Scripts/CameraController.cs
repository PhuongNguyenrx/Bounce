using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float offset;
    public GameObject target;
    void Awake()
    {
        offset = transform.position.y - target.transform.position.y;
    }
    void FixedUpdate()
    {
        Vector3 curPos = transform.position;
        curPos.y = target.transform.position.y + offset;
        transform.position = curPos;
    }
}
