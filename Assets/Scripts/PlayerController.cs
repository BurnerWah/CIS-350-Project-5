/* Robert Krawczyk
 * Project 5
 * Moves from input, stays in boundaries, and shoots from input
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform missileSpawnPosition;
    [SerializeField] GameObject missilePrefab;

    // Settings
    float leftrightSpeed = 7, updownSpeed = 5;
    float missileCooldown = .5f, curr_missileCooldown = 0;
    float rBoundary = 8, lBoundary = -8, upBoundary = 3, downBoundary = -3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * leftrightSpeed * Time.deltaTime);
        transform.Translate(Input.GetAxis("Vertical") * Vector3.up * updownSpeed * Time.deltaTime);

        // Clamp X, clamp Y, keep Z the same
        transform.position = new Vector3( Mathf.Clamp(transform.position.x,lBoundary,rBoundary), Mathf.Clamp(transform.position.y,downBoundary,upBoundary), transform.position.z );

        // Firing Missile
        curr_missileCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && curr_missileCooldown <= 0)
        {
            // Fire missile
            GameObject missile = Instantiate(missilePrefab, missileSpawnPosition.position, missileSpawnPosition.rotation);
            curr_missileCooldown = missileCooldown;
        }
    }
}
