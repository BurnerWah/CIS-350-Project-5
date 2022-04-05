using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    bool locked = false;
    Vector3 lockedPos;
    public static GameObject GetMousePos()
    {
        return GameObject.Find("MousePosition");
    }

    void Awake()
    {
        name = "MousePosition"; // to make sure
        EarlyUpdate();
    }

    void EarlyUpdate()
    {
        if (!locked)
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = new Vector3(mouse.x, mouse.y, 0);
        }
        else
        {
            transform.position = lockedPos;
        }
    }

    void LateUpdate()
    {
        EarlyUpdate();
    }

    public void Lock()
    {
        lockedPos = transform.position;
        locked = true;
    }
}
