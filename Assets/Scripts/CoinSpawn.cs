using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    public GameObject coinPrefab;

    public float timeBetweenSpawns;

    private float timeToNextSpawn;

    void Start()
    {
        timeToNextSpawn = timeBetweenSpawns;
    }

    void Update()
    {
        timeToNextSpawn -= Time.deltaTime;
        if (timeToNextSpawn <= 0.0f)
        {
            Spawn();
            timeToNextSpawn = timeBetweenSpawns;
        }
    }

    void Spawn()
    {
        for(int i = 0; i < 20; i++){
            Instantiate(coinPrefab, new Vector2(-10 + 1.0f * i, 7), Quaternion.identity);
        }
    }
}
