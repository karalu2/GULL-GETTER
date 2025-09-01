using System.Collections;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    [SerializeField] float despawnTimer = 6;
    void Awake()
    {
        StartCoroutine(Despawn());
    }

    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
    }
}
