/*
 * Conner Ogle, Gerard Lamoureux
 * Project 5
 * Simple script that gives the boss some movement
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    float speed = 4;
    Rigidbody2D rb;
    bool movingUp = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            transform.position += transform.up * speed * Time.deltaTime;
            if (transform.position.y >= 1.55)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position -= transform.up * speed * Time.deltaTime;
            if (transform.position.y <= -1.55)
            {
                movingUp = true;
            }
        }
    }

}

