using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public IEnumerator SpawnWave(List<GameObject> queue, List<Transform> spawnLocations, float spawnInterval)
    {
        foreach (var enemyPrefab in queue)
        {
            int spawnLocationId = Random.Range(0, spawnLocations.Count);
            Transform spawnLocation = spawnLocations[spawnLocationId];

            Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public IEnumerator SpawnWave(GameObject enemyPrefab, List<Transform> spawnLocations, float spawnInterval)
    {
        int spawnLocationId = Random.Range(0, spawnLocations.Count);
        Transform spawnLocation = spawnLocations[spawnLocationId];

        Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnInterval);
    }
}
