/*
 * Robert Krawczyk, Jaden Pleasants, Conner Ogle, Gerard Lamoureux
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

    //health for now

    // Settings
    readonly float leftrightSpeed = 7;
    readonly float updownSpeed = 5;
    readonly float missileCooldown = .5f;
    float curr_missileCooldown = 0;
    readonly float rBoundary = 8;
    readonly float lBoundary = -8;
    readonly float upBoundary = 3;
    readonly float downBoundary = -3;

    void Start()
    {

    }

    void Update()
    {
        // Movement
        transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * leftrightSpeed * Time.deltaTime);
        transform.Translate(Input.GetAxis("Vertical") * Vector3.up * updownSpeed * Time.deltaTime);

        // Clamp X, clamp Y, keep Z the same
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, lBoundary, rBoundary), Mathf.Clamp(transform.position.y, downBoundary, upBoundary), transform.position.z);

        // Firing Missile
        curr_missileCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && curr_missileCooldown <= 0)
        {
            // Fire missile
            GameObject missile = Instantiate(missilePrefab, missileSpawnPosition.position, missileSpawnPosition.rotation);
            curr_missileCooldown = missileCooldown;
        }
        if(GameManager.Instance.humanHealth <= 0)
        {
            if(GameManager.Instance.GetCurrentLevel() == "OperationMissionSubmarineBigfish")
            {
                GameManager.Instance.LevelOneGameOver();
            }
            else if(GameManager.Instance.GetCurrentLevel() == "BossLevel")
            {
                GameManager.Instance.BossLevelGameOver(false);
            }
        }
    }
}
