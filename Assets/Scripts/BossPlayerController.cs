/* Gerard Lamoureux
 * Project 5
 * Stops player from exploiting boss attacks (holds them to a certain x value)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 1)
            transform.position = new Vector3(1, transform.position.y, transform.position.z);
    }
}
