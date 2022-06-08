using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;

    Vector3 startPos;
    public Vector3 losePos;

    [SerializeField]
    float bounceForce;


    bool ignoreNextCollision = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        //gets weird after falling too far need fixed position after losing
        if (transform.position.y <= losePos.y-100)
        {
            transform.position = new Vector3(transform.position.x, losePos.y -100, transform.position.z);
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //prevent double collision - > double force
        if (ignoreNextCollision) return;
        //hitting death part
        DeathPart deathPart = other.transform.GetComponent<DeathPart>();
        if (deathPart)
        {
            GameManager.singleton.GameOver();
            return;
        }
        GameManager.singleton.NextPlatform();
        //rb.velocity = Vector3.zero;
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
