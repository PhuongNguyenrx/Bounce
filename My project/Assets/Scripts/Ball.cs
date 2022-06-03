using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float bounceForce;

    bool ignoreNextCollision = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision) return;

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision",0.2f);
    }

    void AllowCollision()
    {
        ignoreNextCollision = false;
    }
}
