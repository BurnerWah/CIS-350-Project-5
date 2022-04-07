/* Robert Krawczyk
 * Project 5
 * Adds force at start
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForce : MonoBehaviour
{
    [SerializeField] Vector2 forceAtStart = new Vector2(-100, 0);

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(forceAtStart);
    }
}
