/* Conner Ogle
 * Project 5
 * Simple script that gives the boss some movement
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    float forcePerSecond = 100000;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //DownForce();
        if (transform.position.y >= 1.8)
        {
            stopForce();
            DownForce();
        }
        else if (transform.position.y <= -1.8)
        {
            stopForce();
            UpForce();
        }
    }
    void UpForce ()
    {
        rb.AddForce(Vector2.up * forcePerSecond * Time.deltaTime);
      
    }
    void DownForce()
    {
        rb.AddForce(Vector2.down * forcePerSecond * Time.deltaTime);
    }
    void stopForce()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
}

