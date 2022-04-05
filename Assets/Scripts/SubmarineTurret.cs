using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineTurret : MonoBehaviour
{
    [SerializeField] GameObject rawAim;
    GameObject mouse;

    // Settings
    float startOffsettingDist = 3f, offsettingRange = 4f;

    // Start is called before the first frame update
    void Start()
    {
        mouse = FollowMouse.GetMousePos();
    }

    // Update is called once per frame
    void Update()
    {
        float percentWithinRange = (Vector3.Distance(transform.position, mouse.transform.position) - startOffsettingDist) / offsettingRange;

        transform.rotation = rawAim.transform.rotation;
        transform.Rotate(new Vector3(0, 0, 20)); // I don't know how to directly rotate a Quaternion by 15 Euler degrees so this obj's transform gets used as a variable lol
        Quaternion offsetAim = transform.rotation;
        transform.rotation = Quaternion.Slerp(rawAim.transform.rotation, offsetAim, percentWithinRange);
    }
}
