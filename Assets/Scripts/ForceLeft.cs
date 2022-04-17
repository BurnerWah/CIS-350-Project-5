/*
 * Robert Krawczyk
 * Project 5
 * Placeholder until open map is implemented. Constantly adds force left
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLeft : MonoBehaviour
{
    float forcePerSecond = 10;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.left * forcePerSecond * Time.deltaTime);
    }
}
