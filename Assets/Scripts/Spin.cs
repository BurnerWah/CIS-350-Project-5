/*
 * Robert Krawczyk
 * Project 5
 * Constantly spinning
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    readonly float degreesPerSecond = -720;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(Vector3.forward * degreesPerSecond * Time.deltaTime);
    }
}
