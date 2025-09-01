using System.Collections;
using Unity.Collections;
using UnityEngine;

public class ProjectileDespawn : MonoBehaviour
{
    [SerializeField] float groundedDespawnTimer = 2;
    [SerializeField] float despawnTimer = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        StartCoroutine(Despawn(despawnTimer));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Despawn(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
