/* Robert Krawczyk
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
    bool offsetting = false; // offsetting can be annoying
    float offsetDegrees = 20, startOffsettingDist = 3f, offsettingRange = 4f;

    // Start is called before the first frame update
    void Start()
    {
        mouse = FollowMouse.GetMousePos();
    }

    // Update is called once per frame
    void Update()
    {
        float percentWithinRange = (Vector3.Distance(transform.position, mouse.transform.position) - startOffsettingDist) / offsettingRange;
        float t = offsetting ? percentWithinRange : 0; // If zero, just shoots straight

        transform.rotation = rawAim.transform.rotation;
        float final_offsetDegrees = offsetDegrees;
        float z = (transform.rotation.eulerAngles.z + 360) % 360;
        if (z < 90 || z >= 270)
        {
            final_offsetDegrees *= -1;
        }
        transform.Rotate(Vector3.forward * final_offsetDegrees);
        Quaternion offsetAim = transform.rotation;
        transform.rotation = Quaternion.Slerp(rawAim.transform.rotation, offsetAim, t);
    }
}
