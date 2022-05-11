/*
 * Conner Ogle, Gerard Lamoureux
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
        float i = 0;
        while (!levelFinished)
        {
            SpawnRandomPrefab();
            i++;
            float delay = (100 - i) / 100;
            if (delay < 0.3f)
                delay = 0.3f;

            Debug.Log(delay);

            yield return new WaitForSeconds(delay);
        }
    }
    // Update is called once per frame
    void Update()
    {


    }

    //spawns cells
    void SpawnRandomPrefab()
    {
        int prefabIndex = Random.Range(0, prefabToSpawn.Length);
        if (prefabIndex == 2)
        {
            if (Random.Range(0, 3) < 2)
            {
                prefabIndex = 1;
            }
        }
        Vector2 SpawnPos;
        //generate a random spawn position
        if (prefabIndex == 2)
        {
            SpawnPos = new Vector2(SpawnPosX, 0);
        }
        else
        {
            SpawnPos = new Vector2(SpawnPosX, Random.Range(bottomBound, upperBound));
        }

        Instantiate(prefabToSpawn[prefabIndex], SpawnPos, prefabToSpawn[prefabIndex].transform.rotation);
    }
}
