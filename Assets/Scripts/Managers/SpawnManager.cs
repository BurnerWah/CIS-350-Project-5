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
        if (GameManager.Instance.level == 1 || GameManager.Instance.level == 2)
            StartCoroutine(SpawnRandomPrefabWithCoroutine());
        else if (GameManager.Instance.level == 3)
            StartCoroutine(BossLevelSpawningCoroutine());
        else if (GameManager.Instance.level == 4)
            StartCoroutine(InfiniteSpawningCoroutine());

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
        int prefabIndex = 0;
        if (GameManager.Instance.level == 1)
        {
            prefabIndex = Random.Range(0,2);
        }
        else if(GameManager.Instance.level == 2)
        {
            prefabIndex = Random.Range(0, 3);
        }
        else if(GameManager.Instance.level == 4)
        {
            prefabIndex = Random.Range(0, 4);
        }
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

    //Boss Coroutine
    IEnumerator BossLevelSpawningCoroutine()
    {
        int levelScore;
        yield return new WaitForSeconds(1.5f);
        for(int i=0;i<40;i++)
        {
            SpawnBossPrefab(false);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2f);
        levelScore = GameManager.Instance.score;
        SpawnBossPrefab(true);
        while(levelScore == GameManager.Instance.score)
            yield return new WaitForSeconds(1f);
        for (int i = 0; i < 40; i++)
        {
            SpawnBossPrefab(false);
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(2f);
        levelScore = GameManager.Instance.score;
        SpawnBossPrefab(true);
        yield return new WaitForSeconds(1f);
        while (levelScore == GameManager.Instance.score)
        {
            yield return new WaitForSeconds(0.5f);
            SpawnBossPrefab(false);
        }
        for (int i = 0; i < 40; i++)
        {
            SpawnBossPrefab(false);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(2f);
        levelScore = GameManager.Instance.score;
        SpawnBossPrefab(true);
        yield return new WaitForSeconds(1f);
        while (levelScore == GameManager.Instance.score)
        {
            yield return new WaitForSeconds(0.3f);
            SpawnBossPrefab(false);
        }
        GameManager.Instance.BossLevelGameOver(true);
    }

    void SpawnBossPrefab(bool spawnBoss)
    {
        int prefabIndex;
        if(spawnBoss)
        {
            prefabIndex = 3;
        }
        else
        {
            prefabIndex = Random.Range(0, 3);
        }
        
        if (prefabIndex == 2)
        {
            if (Random.Range(0, 3) < 2)
            {
                prefabIndex = 1;
            }
        }
        Vector2 SpawnPos;
        //generate a random spawn position
        if (prefabIndex == 2 || prefabIndex == 3)
        {
            SpawnPos = new Vector2(SpawnPosX, 0);
        }
        else
        {
            SpawnPos = new Vector2(SpawnPosX, Random.Range(bottomBound, upperBound));
        }

        Instantiate(prefabToSpawn[prefabIndex], SpawnPos, prefabToSpawn[prefabIndex].transform.rotation);
    }

    IEnumerator InfiniteSpawningCoroutine()
    {
        yield return new WaitForSeconds(3f);
        float delay = 1.5f;
        for (float i=0;i<50;i++)
        {
            SpawnInfinitePrefab(1);
            delay = (200 - (i*2)) / 200;
            if (delay < 0.5f)
                delay = 0.5f;
            yield return new WaitForSeconds(delay);
        }
        for (float i = 50; i < 100; i++)
        {
            SpawnInfinitePrefab(2);
            delay = (200 - (i*2)) / 200;
            if (delay < 0.5f)
                delay = 0.5f;
            yield return new WaitForSeconds(delay);
        }
        SpawnInfinitePrefab(3);
        yield return new WaitForSeconds(3f);
        while(true)
        {
            delay -= 0.02f;
            if (delay < 0.3f)
                delay = 0.3f;
            for(int i=0;i<75;i++)
            {
                SpawnInfinitePrefab(2);
                yield return new WaitForSeconds(delay);
            }
            SpawnInfinitePrefab(3);
            yield return new WaitForSeconds(2f);
        }
    }

    void SpawnInfinitePrefab(int level)
    {
        int prefabIndex = 0;
        if (level == 1)
        {
            prefabIndex = Random.Range(0, 2);
        }
        else if(level == 2)
        {
            prefabIndex = Random.Range(0, 3);
        }
        else if(level == 3)
        {
            prefabIndex = 3;
        }

        if (prefabIndex == 2)
        {
            if (Random.Range(0, 3) < 2)
            {
                prefabIndex = 1;
            }
        }
        Vector2 SpawnPos;
        //generate a random spawn position
        if (prefabIndex == 2 || prefabIndex == 3)
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
