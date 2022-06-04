using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.red;
        GetComponent<Collider>().isTrigger = true;
    }

}
