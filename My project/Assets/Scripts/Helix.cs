using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helix : MonoBehaviour
{
    private float lastPos;
    private float rotateAngle;
    private Vector3 startRotation;

    private void Awake()
    {
        startRotation = transform.localEulerAngles;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            float currentPos = Input.mousePosition.x;

            if (lastPos == 0)
            {
                lastPos = currentPos;
            }
            rotateAngle = lastPos - currentPos;
            lastPos = currentPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastPos = 0;
        }
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, rotateAngle,0);
    }
}
