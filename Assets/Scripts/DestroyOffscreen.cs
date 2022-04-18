/*
 * Robert Krawczyk, Gerard Lamoureux
 * Project 5
 * Destroys self when out of bounds
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    float leftrightBoundary = 11, updownBoundary = 8;

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > leftrightBoundary || Mathf.Abs(transform.position.y) > updownBoundary)
        {
            if(gameObject.CompareTag("Enemy"))
            {
                Debug.Log(GameManager.Instance.humanHealth);
                GameManager.Instance.humanHealth--;
            }
            Destroy(gameObject);
        }
    }
}
