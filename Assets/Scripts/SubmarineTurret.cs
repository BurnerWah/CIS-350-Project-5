/*
 * Robert Krawczyk, Jaden Pleasants
 * Project 5
 * Aims at mouse, a little bit off at further ranges so missile curves
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineTurret : MonoBehaviour
{
    [SerializeField] GameObject rawAim;
    GameObject mouse;

    // Settings
    // offsetting can be annoying
    public readonly bool offsetting = false;
    readonly float offsetDegrees = 20;
    readonly float startOffsettingDist = 3f;
    readonly float offsettingRange = 4f;

    void Start()
    {
        mouse = FollowMouse.GetMousePos();
    }

    void Update()
    {
        float percentWithinRange = (Vector3.Distance(transform.position, mouse.transform.position) - startOffsettingDist) / offsettingRange;
        // If zero, just shoots straight
        float t = offsetting ? percentWithinRange : 0;

        // This object's final rotation ends up somewhere between the rawAim object's rotation (shooting straight), and that angle +/- 20 degrees
        transform.rotation = rawAim.transform.rotation;
        float final_offsetDegrees = offsetDegrees;
        // should end up with values in between 0 and 360
        float z = (transform.rotation.eulerAngles.z + 360) % 360;
        // offset up if aiming up, offset down if aiming down (not working rn)
        if (z < 90 || z >= 270)
        {
            final_offsetDegrees *= -1;
        }
        transform.Rotate(Vector3.forward * final_offsetDegrees); // +/= 20 degrees
        Quaternion offsetAim = transform.rotation;
        // somewhere in between (t is the percentage)
        transform.rotation = Quaternion.Slerp(rawAim.transform.rotation, offsetAim, t);
    }
}
