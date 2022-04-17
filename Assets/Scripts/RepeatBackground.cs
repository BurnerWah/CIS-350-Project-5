/*
 * Gerard Lamoureux
 * Project 5
 * Handles background repeating
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.right * 10 * Time.deltaTime;
        if (transform.position.x < startPosition.x - repeatWidth)
        {
            transform.position = startPosition;
        }
    }
}
