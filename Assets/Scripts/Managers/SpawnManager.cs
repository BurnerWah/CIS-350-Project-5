/*
 * Conner Ogle
 * Project 5
 * Handles Spawn management
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    //set this array of references in the inspector
    public GameObject[] prefabToSpawn;

    //variables for spawn position
    private float bottomBound = -3.5f;
    private float upperBound = 2.5f;
    private float SpawnPosX = 10;

    bool levelFinished;


    void Start()
    {
        
        StartCoroutine(SpawnRandomPrefabWithCoroutine());

    }
    //coroutine spawn stuff
    IEnumerator SpawnRandomPrefabWithCoroutine()
    {
        //add a 1.5 second delay befire first spawning objects
        yield return new WaitForSeconds(1.5f);

        while (!levelFinished)
        {
            SpawnRandomPrefab();

            float RandomDelay = Random.Range(0.5f, 1f);

            yield return new WaitForSeconds(RandomDelay);
        }
    }
    // Update is called once per frame
    void Update()
    {


    }

    //spawns cells
    void SpawnRandomPrefab()
    {
        //pick a cell index
        int prefabIndex = Random.Range(0, prefabToSpawn.Length);

        //generate a random spawn position
        Vector2 SpawnPos = new Vector2(SpawnPosX, Random.Range(bottomBound,upperBound));

        Instantiate(prefabToSpawn[prefabIndex], SpawnPos, prefabToSpawn[prefabIndex].transform.rotation);
    }
}
