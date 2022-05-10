/*
 * Robert Krawczyk, Jaden Pleasants, Gerard Lamoureux
 * Project 5
 * Dash and get BIG every few seconds
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
    float restingScale = 1;
    float attackScale = 2f;
    float attackTransitionTime = .2f;
    float attackDuration = .5f;
    float attackForce = 350;
    float attackCooldown = 2.0f;
    float sightRange = 10;
    float playerHitForce = 50;

    // Backend
    float curr_attackCooldown = 2, curr_attackTime = 0;
    bool attacking = false, inRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        curr_attackCooldown -= Time.deltaTime;

        // Close enough to player?
        if (Vector2.Distance(transform.position, player.transform.position) <= sightRange && transform.position.x > -7)
        {
            if (!inRange || curr_attackCooldown <= 0)
            {
                inRange = true;
                // If player in range, attack
                curr_attackCooldown = attackCooldown;
                StartAttack();
            }
        }
        else
        {
            if (inRange)
            {
                // Exiting sight range
                inRange = false;
            }
        }
        

        // While attacking
        if (attacking)
        {
            curr_attackTime += Time.deltaTime;

            if (curr_attackTime <= attackTransitionTime)
            {
                // Getting BIG
                transform.localScale = Vector3.one * Mathf.Lerp(restingScale, attackScale, (curr_attackTime - 0) / attackTransitionTime);
            }
            else if (curr_attackTime > attackDuration && curr_attackTime <= attackDuration + attackTransitionTime)
            {
                // Getting back to normal
                transform.localScale = Vector3.one * Mathf.Lerp(attackScale, restingScale, (curr_attackTime - attackDuration) / attackTransitionTime);
            }
            else if (curr_attackTime > attackDuration + attackTransitionTime)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce( playerHitForce*( collision.gameObject.transform.position - transform.position ) );
        }
    }
}
