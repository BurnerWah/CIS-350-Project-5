/*
 * Gerard Lamoureux
 * Project 5
 * Adjusts the camera for the story
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCamera : MonoBehaviour
{
    [SerializeField] private GameObject ClickManager;
    StoryClicker clicker;
    int moment = 0;
    // Start is called before the first frame update
    void Start()
    {
        clicker = ClickManager.GetComponent<StoryClicker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(clicker.moment == 4 && moment != 4)
        {
            moment = 4;
            gameObject.GetComponent<Camera>().orthographicSize = 18;
            transform.position = new Vector3(transform.position.x, -7, transform.position.z);
        }
        if(clicker.moment == 6 && moment != 6)
        {
            moment = 6;
            gameObject.GetComponent<Camera>().orthographicSize = 3;
            transform.position = new Vector3(-6.1f, -3.5f, transform.position.z);
        }
    }
}
