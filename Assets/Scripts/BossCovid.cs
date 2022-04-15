/* Conner Ogle
 * Project 5
 * Script for Boss Object, shoots covid cells at player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCovid : MonoBehaviour
{
    //boss health, 10 hits to kill
    public int BossHealth = 10;

    //variables
    public GameObject player;
    [SerializeField] GameObject CovidCell;
    [SerializeField] Transform AttackSpawnPosition;
    Rigidbody2D covidRb;
    float attackCooldown = 1.5f, curr_attackCooldown = 0, attackForce = 300;

    //make boss speak every couple of attacks, not implemented
    public GameObject bossSpeech;





    // Start is called before the first frame update
    void Start()
    {
        covidRb = CovidCell.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        attack();
        if (BossHealth == 0)
        {
            Death();
        }
        curr_attackCooldown -= Time.deltaTime;
    }

    void Death()
    {
        //celebration or something
        Destroy(gameObject);
    }
   
    //here the boss shoots covid cells at player, currently has no effect on the player if hit. These cells can still be destroyed; gameplay could involve having to destroy them to survive or we change the prefab so that the player has to dodge them
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
