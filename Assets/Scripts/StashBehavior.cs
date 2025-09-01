using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StashBehavior : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float refreshTimer = 2f;
    [SerializeField] float checkRadius = 1f;
    [SerializeField] string targetTag = "Enemy";
    [SerializeField] HealthBar healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CheckEnemyCollision());
    }

    public IEnumerator CheckEnemyCollision()
    {
        while (true)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, checkRadius);
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag(targetTag) && hit.gameObject != gameObject)
                {
                    health--;
                    healthBar.SetHealth(health);
                    if (health == 0)
                    {
                        SceneManager.LoadScene("Game Over");
                    }
                }
            }

            yield return new WaitForSeconds(refreshTimer);
        }
    }
}
