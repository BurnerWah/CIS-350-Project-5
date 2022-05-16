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
    float leftrightBoundary = 11;
    float updownBoundary = 8;

    void Update()
    {
        if (transform.position.x < (-leftrightBoundary))
        {
            if (gameObject.CompareTag("Enemy"))
            {
                Debug.Log(GameManager.Instance.humanHealth);
                GameManager.Instance.humanHealth--;
                FindObjectOfType<UIManager>().DamageUI();
            }
            Destroy(gameObject);
        }
        else if(transform.position.x > leftrightBoundary || Mathf.Abs(transform.position.y) > updownBoundary)
        {
            Destroy(gameObject);
        }
    }
}
