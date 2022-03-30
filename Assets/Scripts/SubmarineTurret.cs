using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineTurret : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)); // not right?
        transform.LookAt(new Vector3(mouse.x, mouse.y, transform.position.z), transform.position + Vector3.up);
    }
}
