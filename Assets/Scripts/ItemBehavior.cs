using System.Collections;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    [SerializeField] float despawnTimer = 6;
    void Awake()
    {
        StartCoroutine(DespawnTimer());
    }

    public IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
    }
}
