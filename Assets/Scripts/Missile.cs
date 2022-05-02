/*
 * Robert Krawczyk, Conner Ogle, Jaden Pleasants, Gerard Lamoureux
 * Project 5
 * Targets, moves, turns slightly, explodes and kills
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Missile : MonoBehaviour
{
    // References
    [SerializeField] GameObject idealAngleObj;
    [SerializeField] FollowMouse followMouse;
    public BossCovid BossCovidScript;
    SubmarineTurret turret;

    // Settings
    public float speed = 15, aimSecondsEarly = 0.01f;


    // Backend
    Quaternion startingAngle;
    float timeAlive = 0;
    float targetingTimePerDistance;
    float targetingDuration;
    bool onTarget = false;
    bool sticking;

    // Start is called before the first frame update
    void Start()
    {

        startingAngle = transform.rotation;

        // Lock target
        followMouse.Lock();

        // Determine targeting duration (can't be zero)
        targetingDuration = Mathf.Max((1 / speed) * Vector3.Distance(transform.position, followMouse.transform.position) - aimSecondsEarly, 0);

        // save computation time if shooting straight
        if (turret.offsetting || startingAngle == idealAngleObj.transform.rotation)
        {
            onTarget = true;
        }
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        turret = FindObjectOfType<SubmarineTurret>();
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
        else if (obj.CompareTag("Enemy"))
        {
            GameManager.Instance.score++;
            FindObjectOfType<UIManager>().ScoreUI();
            print("Covid Killed: ");
            Destroy(obj);
            Explode();
        }
        //added boss tag, decrements his health by 1 each hit
        else if (obj.CompareTag("Boss"))
        {
            obj.GetComponent<BossCovid>().BossHealth--;
            obj.GetComponent<BossCovid>().HealthBar.UpdateHealth();
            print(obj.GetComponent<BossCovid>().BossHealth);
            Explode();
        }
        else if (obj.CompareTag("Friend"))
        {
            print("hit red blood cell. lose score or something");
            GameManager.Instance.humanHealth--;
            FindObjectOfType<UIManager>().DamageUI();
            Destroy(obj);
            Explode();
        }
    }
    void OnCollisionEnter2D(Collision2D collision) => OnTrigger2DOrCollision2D(collision.gameObject);
    void OnTriggerEnter2D(Collider2D collision) => OnTrigger2DOrCollision2D(collision.gameObject);

    void Explode()
    {
        // animation or smth
        Destroy(gameObject);
    }
}
