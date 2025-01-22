using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    public GameObject gasPrefab;
    public float spawnTime = 2f;
    public float firstSpawnX = 30f;
    public float nextSpawnXPos = 30f;
    public float nextSpawnRandomRange = 1.5f;
    public float roadWidth = 4f;
    public Transform player;
    public float cleanDistance = 50f;

    private float[] spawnPositions = { -1.2f, 0, 1.2f };
    private float nextSpawnX;
    private List<GameObject> spawnedGases = new List<GameObject>();
    void Start()
    {
        nextSpawnX = firstSpawnX;
            
        InvokeRepeating(nameof(SpawnGas), 0f, spawnTime);
        InvokeRepeating(nameof(CleanGas), 1f, 1f);
    }

    void SpawnGas()
    {
        float randomizedSpawnX = nextSpawnX + Random.Range(-nextSpawnRandomRange, nextSpawnRandomRange);

        float spawnZ = spawnPositions[Random.Range(0, spawnPositions.Length)];
        Vector3 spawnPosition = new Vector3(randomizedSpawnX, 0f, spawnZ);

        GameObject gas = Instantiate(gasPrefab, spawnPosition, Quaternion.identity);
        spawnedGases.Add(gas);
        
        nextSpawnX += nextSpawnXPos;
    }

    void CleanGas()
    {
        if (player != null) return;

        for (int i = spawnedGases.Count - 1; i >= 0; i-- )
        {
            if (spawnedGases[i] != null)
            {
                float distance = player.position.x - spawnedGases[i].transform.position.x;
                if (distance > cleanDistance)
                {
                    Destroy(spawnedGases[i]);
                    spawnedGases.RemoveAt(i);
                }
            }
        }
    }
}
