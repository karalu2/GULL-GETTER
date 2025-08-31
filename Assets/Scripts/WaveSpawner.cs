using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using WaveRecord = System.Collections.Generic.Dictionary<string, int>;

public class WaveSpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    private WaveRecord waveTracker = new WaveRecord();
    
    private Dictionary<int, WaveRecord> waveLimits;

    public int income;
    public int currWave;
    private const int waveInterval = 5;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public List<Transform> spawnLocations = new List<Transform>();
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    void Start()
    {
        waveTracker.Add("Normal", 0);
        waveTracker.Add("Drunk", 0);
        waveTracker.Add("Starving", 0);
        waveTracker.Add("Boss", 0);

        setWaveLimits();
        GenerateWave();
    }

    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                int spawnLocationId = Random.Range(0, spawnLocations.Count);
                Transform spawnLocation = spawnLocations[spawnLocationId];
                Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
            }
            else
            {
                waveTimer = 0;
            }

        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * income;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();

        int waveBase = (int)System.Math.Floor((double)currWave / waveInterval) * waveInterval;
        if (waveBase > waveLimits.Keys.Last())
        {
            waveBase = waveLimits.Keys.Last();
        }

        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            GameObject enemyPrefab = enemies[randEnemyId].enemyPrefab;
            string enemyTag = enemyPrefab.tag;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemyPrefab);
                waveValue -= randEnemyCost;
                waveTracker[enemyTag] += 1;
            }
            else if (waveTracker[enemyTag] == waveLimits[waveBase][enemyTag])
            {
                continue;
            }
            else if (waveValue == 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            waveTracker[i] = 0;
        }
    }

    public void setWaveLimits()
    {
        waveLimits = new Dictionary<int, WaveRecord>()
        {
            {0, new WaveRecord { {0, 0}, {1, 0}, {2, 0}, {3, 0}} },
            {3, new WaveRecord {}}
            {10, new WaveRecord }
        };
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}