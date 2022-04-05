using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // References
    [SerializeField] GameObject idealAngleObj;
    [SerializeField] FollowMouse followMouse;

    // Settings
    float speed = 15, aimSecondsEarly = .25f;

    // Backend
    Quaternion startingAngle;
    float timeAlive = 0, targetingTimePerDistance, targetingDuration;
    bool onTarget = false;
    bool sticking;

    // Start is called before the first frame update
    void Start()
    {
        startingAngle = transform.rotation;

        // Lock target
        followMouse.Lock();

        // Determine targeting duration
        targetingDuration = Mathf.Max( (1/speed) * Vector3.Distance(transform.position, followMouse.transform.position) - aimSecondsEarly, 0 ); // can't be negative
    }

    // Update is called once per frame
    void Update()
    {
        if (!sticking)
        {
            // Move forward
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            // Rotate if targeting
            timeAlive += Time.deltaTime;
            float percentOnTarget = timeAlive / targetingDuration;
            if (percentOnTarget <= 1)
            {
                transform.rotation = Quaternion.Slerp(startingAngle, idealAngleObj.transform.rotation, percentOnTarget);
            }
            else if(!onTarget)
            {
                Destroy(idealAngleObj);
                Destroy(followMouse.gameObject);
                onTarget = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        print("hit");
        if(collision.gameObject.tag == "Wall")
        {
            sticking = true;
            GetComponent<BoxCollider2D>().enabled = false;
            transform.parent = collision.transform;
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            // make other die
            // gain score or something
            Explode();
        }
        else if(collision.gameObject.tag == "Friend")
        {
            // make other die
            // lose score or something
            Explode();
        }
        
    }

    void Explode()
    {
        // animation or smth
        Destroy(gameObject);
    }
}
