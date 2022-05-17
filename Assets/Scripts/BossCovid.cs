/*
 * Conner Ogle, Jaden Pleasants, Gerard Lamoureux
 * Project 5
 * Script for Boss Object, shoots covid cells at player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCovid : MonoBehaviour
{
    // boss health, 10 hits to kill
    public int BossMaxHealth = 10;
    private int bossHealth;


    // variables
    public GameObject player;
    [SerializeField] GameObject CovidCell;
    [SerializeField] Transform AttackSpawnPosition;
    public HealthBar HealthBar;
    Rigidbody2D covidRb;
    float attackCooldown = 1.5f;
    float curr_attackCooldown = 1.5f;
    float attackForce = 100;

    // make boss speak every couple of attacks, not implemented
    public GameObject bossSpeech;

    public int BossHealth { get => bossHealth; set => bossHealth = value; }

    void Start()
    {
        if (GameManager.Instance.level == 4)
            BossMaxHealth = 5;
        BossHealth = BossMaxHealth;
        covidRb = CovidCell.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("/Player");
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - (Time.deltaTime * 1.5f), 0, gameObject.transform.position.z);
        attack();
        if (BossHealth == 0)
        {
            Death();
            //GameManager.Instance.BossLevelGameOver(true);
        }
        curr_attackCooldown -= Time.deltaTime;
    }

    void Death()
    {
        GameManager.Instance.score++;
        // celebration or something
        Destroy(gameObject);
    }

    // here the boss shoots covid cells at player, currently has no effect on the player if hit.
    // These cells can still be destroyed;
    // gameplay could involve having to destroy them to survive or we change the prefab so that the
    // player has to dodge them
    void attack()
    {
        if (curr_attackCooldown <= 0)
        {
            GameObject covidAttack = Instantiate(CovidCell, AttackSpawnPosition.position, AttackSpawnPosition.rotation);
            covidRb = covidAttack.gameObject.GetComponent<Rigidbody2D>();
            covidRb.AddForce(attackForce * Vector3.Normalize(player.transform.position - covidAttack.transform.position));
            curr_attackCooldown = attackCooldown;
        }
    }
}
