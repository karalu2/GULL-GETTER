using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideManager : MonoBehaviour
{
    // [SerializeField] List<Item> items = new List<Item>();
    [SerializeField] GameObject fishPrefab;

    [SerializeField] float tideCooldown = 7;
    [SerializeField] float tideDuration = 3;
    [SerializeField] Animator tide;

    private ItemSpawner itemSpawner;
    private Coroutine tideCoroutine;

    [SerializeField] List<Transform> spawnLocations = new List<Transform>();

    void Start()
    {
        itemSpawner = GetComponent<ItemSpawner>();
        tideCoroutine = StartCoroutine(TideLoop());
    }
    public IEnumerator TideLoop()
    {
        while (true)
        {
            tide.SetBool("rise", true);
            tide.SetBool("wait", true);

            // yield return StartCoroutine(itemSpawner.SpawnItem(items, spawnLocations));
            yield return new WaitForSeconds(tideDuration);

            int spawnLocationId = Random.Range(0, spawnLocations.Count);
            Transform spawnLocation = spawnLocations[spawnLocationId];

            Instantiate(fishPrefab, spawnLocation.position, Quaternion.identity);

            tide.SetBool("wait", false);
            tide.SetBool("rise", false);

            yield return new WaitForSeconds(tideCooldown);
        }
    }

/*
    public GameObject SelectItem()
    {
        int totalWeight = 0;
        foreach (Item item in items)
        {
            totalWeight += item.GetWeight();
        }

        int randomValue = Random.Range(0, totalWeight);

        foreach (Item item in items)
        {
            if (randomValue < item.GetWeight())
            {
                GameObject itemPrefab = item.GetPrefab();
                return itemPrefab;
            }
            randomValue -= item.GetWeight();
        }

        return items[0].GetPrefab();
    }
    */

    [System.Serializable]
    private class Item
    {
        [SerializeField] GameObject itemPrefab;
        [SerializeField] int weight;
        public GameObject GetPrefab() { return itemPrefab; }
        public int GetWeight() { return weight; }
    }
}
