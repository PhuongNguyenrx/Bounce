using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;

    Vector3 startPos;

    [SerializeField]
    float bounceForce;


    bool ignoreNextCollision = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }
    void OnCollisionEnter(Collision other)
    {
        if (ignoreNextCollision) return;
        DeathPart deathPart = other.transform.GetComponent<DeathPart>();
        if (deathPart)
        {
            GameManager.singleton.GameOver();
            return;
        }
        GameManager.singleton.NextPlatform();
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision",0.2f);
    }

    void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetPosition()
    {
        transform.position = startPos;
    }
}
