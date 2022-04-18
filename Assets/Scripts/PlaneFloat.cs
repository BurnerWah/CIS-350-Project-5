/*
 * Gerard Lamoureux
 * Project 5
 * Allows plane to "float"
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFloat : MonoBehaviour
{
    private float RotateSpeed = 3f;
    private float Radius = 0.2f;

    private Vector2 center;
    private float angle;

    private void Start()
    {
        center = transform.position;
    }

    private void Update()
    {

        angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        transform.position = center + offset;
    }
}
