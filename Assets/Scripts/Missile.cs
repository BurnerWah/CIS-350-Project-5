/* Robert Krawczyk, Conner Ogle
 * Project 5
 * Targets, moves, turns slightly, explodes and kills
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // References
    [SerializeField] GameObject idealAngleObj;
    [SerializeField] FollowMouse followMouse;
    public BossCovid BossCovidScript;

    // Settings
    public float speed = 15, aimSecondsEarly = 0.01f;

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

        if(startingAngle == idealAngleObj.transform.rotation) { onTarget = true; } // save computation time if shooting straight

        BossCovidScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossCovid>();
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
            if (!onTarget)
            {
                float percentOnTarget = timeAlive / targetingDuration;
                if (!onTarget && percentOnTarget < 1)
                {
                    transform.rotation = Quaternion.Slerp(startingAngle, idealAngleObj.transform.rotation, percentOnTarget);
                }
                else
                {
                    Destroy(idealAngleObj);
                    Destroy(followMouse.gameObject);
                    onTarget = true;
                }
            }
        }
        
    }
    
    void OnTrigger2DOrCollision2D(GameObject obj)
    {
        if (obj.CompareTag("Wall"))
        {
            print("hit wall. stick to wall");
            sticking = true;
            GetComponent<BoxCollider2D>().enabled = false;
            transform.parent = obj.transform;
        }
        else if(obj.CompareTag("Enemy"))
        {
            print("hit covid. gain score or something");
            Destroy(obj);
            Explode();
        }
        //added boss tag, decrements his health by 1 each hit
        else if (obj.CompareTag("Boss"))
        {
            BossCovidScript.BossHealth = BossCovidScript.BossHealth-1;
            print(BossCovidScript.BossHealth);
            Explode();
        }
        else if(obj.CompareTag("Friend"))
        {
            print("hit red blood cell. lose score or something");
            Destroy(obj);
            Explode();
        }
    }
    void OnCollisionEnter2D(Collision2D collision) { OnTrigger2DOrCollision2D(collision.gameObject); }
    private void OnTriggerEnter2D(Collider2D collision) { OnTrigger2DOrCollision2D(collision.gameObject); }

    void Explode()
    {
        // animation or smth
        Destroy(gameObject);
    }
}
