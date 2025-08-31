using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    [SerializeField] int income;
    private int currWave;
    private int waveValue;
    [SerializeField] int waveInterval = 5;

    private float spawnTimer;
    private float spawnInterval;
    [SerializeField] float spawnIntervalMin = 1.5f;
    private List<GameObject> spawnQueue = new List<GameObject>();
    [SerializeField] List<Transform> spawnLocations = new List<Transform>();

    void Start()
    {
        currWave = 1;
        spawnInterval = 5;
        waveValue = currWave * income;
        spawnTimer = spawnInterval;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            QueueEnemies();
        }
        SpawnWave();
    }

    public void SpawnWave()
    {
        if (spawnTimer <= 0)
        {
            if (spawnQueue.Count > 0)
            {
                int spawnLocationId = Random.Range(0, spawnLocations.Count);
                Transform spawnLocation = spawnLocations[spawnLocationId];
                Instantiate(spawnQueue[0], spawnLocation.position, Quaternion.identity);
                spawnQueue.RemoveAt(0);

                spawnTimer = spawnInterval;
            }
            else
            {
                currWave += 1;
                if (currWave % waveInterval == 0)
                {
                    spawnInterval = (spawnInterval == spawnIntervalMin ? spawnInterval : spawnInterval - 0.5f);
                }
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }
    }

    public void QueueEnemies()
    {
        List<GameObject> queue = new List<GameObject>();

        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            GameObject enemyPrefab = enemies[randEnemyId].GetPrefab();
            int enemyCost = enemies[randEnemyId].GetCost();

            if (waveValue - enemyCost >= 0)
            {
                queue.Add(enemyPrefab);
                waveValue -= enemyCost;
            }
            else if (waveValue == 0)
            {
                break;
            }
        }

        spawnQueue.Clear();
        spawnQueue = queue;
    }

    [System.Serializable]
    private class Enemy
    {
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] int cost;

        public GameObject GetPrefab() { return enemyPrefab; }
        public int GetCost() { return cost; }
    }
}