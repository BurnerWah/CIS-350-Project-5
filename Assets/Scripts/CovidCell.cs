/* Robert Krawczyk
 * Project 5
 * Dash and melee attack auto
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovidCell : MonoBehaviour
{
    // References
    public GameObject player;
    Rigidbody2D rb;

    // Settings
    [SerializeField] float attackScale = 1.3f, attackTransitionTime = .5f, attackDuration = 1.5f, attackForce = 150, attackCooldown = 5, sightRange = 3;

    // Backend
    float curr_attackCooldown=0, curr_attackTime=0;
    bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player"); // Eventually remove this and have the Spawn Manager set it for me, to save on computation
    }

    // Update is called once per frame
    void Update()
    {
        // Time to start attack?
        curr_attackCooldown -= Time.deltaTime;
        if(curr_attackCooldown <= 0)
        {
            curr_attackCooldown = attackCooldown;
            // If player in range, attack
            if(Vector2.Distance(transform.position, player.transform.position) <= sightRange)
            {
                StartAttack();
            }
        }
        
        // While attacking
        if(attacking)
        {
            curr_attackTime += Time.deltaTime;

            if(curr_attackTime <= attackTransitionTime)
            {
                // Getting BIG
                transform.localScale = Vector3.one * Mathf.Lerp(1, attackScale, (curr_attackTime-0) / attackTransitionTime);
            }
            else if(curr_attackTime > attackDuration && curr_attackTime <= attackDuration + attackTransitionTime)
            {
                // Getting back to normal
                transform.localScale = Vector3.one * Mathf.Lerp(attackScale, 1, (curr_attackTime-attackDuration) / attackTransitionTime);
            }
            else if(curr_attackTime > attackDuration + attackTransitionTime)
            {
                // Back to normal
                attacking = false;
                transform.localScale = Vector3.one;
            }
        }
    }

    void StartAttack()
    {
        // Dash
        rb.AddForce(attackForce * Vector3.Normalize(player.transform.position - transform.position));

        attacking = true;
        curr_attackTime = 0;
    }
}
