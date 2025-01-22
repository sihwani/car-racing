using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject highWayPrefab;
    public int pathCount = 2;
    public float pathLength = 10;
    public Transform player;

    private Queue<GameObject> pathPool = new Queue<GameObject>();
    private float lastSpawnPosx;

    void Start()
    {
        for (int i = 0; i < pathCount; i++)
        {
            SpawnRoad();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player.position.x > lastSpawnPosx - (pathLength * (pathCount - 1)))
        {
            SpawnRoad();
            RespawnRoad();
        }
    }

    void SpawnRoad()
    {
        GameObject newRoad;
        if (pathPool.Count > 0)
        {
            newRoad = pathPool.Dequeue();
            newRoad.SetActive(true);
        }
        else
        {
            newRoad = Instantiate(highWayPrefab);
        }

        newRoad.transform.position = new Vector3(lastSpawnPosx, -1, 0);
        lastSpawnPosx += pathLength;
    }

    void RespawnRoad()
    {
        GameObject oldRoad = GameObject.FindGameObjectsWithTag("Road")[0];
        oldRoad.SetActive(false);
        pathPool.Enqueue(oldRoad);
    }
}

