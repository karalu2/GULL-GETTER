using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    [SerializeField] List<Transform> spawnLocations = new List<Transform>();

    [SerializeField] int income = 5;
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] float spawnIntervalMin = 1f;
    [SerializeField] float waveCooldown = 5f;

    private int currWave = 1;
    private int waveValue;

    private WaveSpawner spawner;
    private Coroutine waveCoroutine;

    void Start()
    {
        spawner = GetComponent<WaveSpawner>();
        waveValue = currWave * income;
        waveCoroutine = StartCoroutine(WaveLoop());
    }

    public IEnumerator WaveLoop()
    {
        while (true)
        {
            List<GameObject> queue = BuildQueue();

            yield return StartCoroutine(spawner.SpawnWave(queue, spawnLocations, spawnInterval));

            currWave++;
            waveValue = currWave * income;
            spawnInterval = Mathf.Max(spawnInterval - 0.5f, spawnIntervalMin);

            Debug.Log($"Wave {currWave} complete, spawning next in {waveCooldown}s");

            yield return new WaitForSeconds(waveCooldown);
        }
    }

    public List<GameObject> BuildQueue()
    {
        List<GameObject> queue = new List<GameObject>();
        int budget = waveValue;

        while (budget > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            GameObject enemyPrefab = enemies[randEnemyId].GetPrefab();
            int enemyCost = enemies[randEnemyId].GetCost();

            if (budget - enemyCost >= 0)
            {
                queue.Add(enemyPrefab);
                budget -= enemyCost;
            }
            else if (budget == 0)
            {
                break;
            }
        }

        return queue;
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
