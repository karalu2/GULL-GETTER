using System.Collections;
using Unity.Collections;
using UnityEngine;

public class ProjectileDespawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            StartCoroutine(Despawn());
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
