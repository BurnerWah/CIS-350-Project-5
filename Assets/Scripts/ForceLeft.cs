/*
 * Robert Krawczyk, Conner Ogle
 * Project 5
 * Placeholder until open map is implemented. Constantly adds force left
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLeft : MonoBehaviour
{
    float forcePerSecond = 100;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.AddForce(Vector2.left * forcePerSecond * Time.deltaTime);
    }
}
