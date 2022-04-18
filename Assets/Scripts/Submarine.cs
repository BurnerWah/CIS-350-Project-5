using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    public int playerHealth = 10;
    bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 0)
        {
            gameOver = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth = playerHealth - 1;
            Destroy(collision.gameObject);
            Debug.Log("Player Health: " + playerHealth);
        }
    }
}
