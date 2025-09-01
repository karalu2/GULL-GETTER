using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StashBehavior : MonoBehaviour
{
    public float health;
    [SerializeField] float refreshTimer = 2f;
    [SerializeField] float checkRadius = 1f;
    [SerializeField] string targetTag = "Enemy";
    [SerializeField] HealthBar healthBar;
    [SerializeField] string gameOverScene = "Game Over";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = healthBar.GetMaxHealth();
        healthBar.SetHealth(health);
        // StartCoroutine(CheckEnemyCollision());
    }

    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.T))
    //    {
    //        health -= 10;
    //        healthBar.SetHealth(health);
    //        if (health == 0)
    //        {
    //            SceneManager.LoadScene(gameOverScene);
    //        }
    //    }
    //}

    public void TakeDamage(float damage)
    {
        health = Math.Max(0, health - damage);
        healthBar.SetHealth(health);
        if (health == 0)
        {
            SceneManager.LoadScene(gameOverScene);
        }
    }
    /*
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(targetTag))
        {
            health--;
            healthBar.SetHealth(health);
            if (health == 0)
            {
                SceneManager.LoadScene(gameOverScene);
            }
            Destroy(collider.gameObject);
        }
    }
    */
    /*
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
                        SceneManager.LoadScene(gameOverScene);
                    }
                }
            }

            yield return new WaitForSeconds(refreshTimer);
        }
    }
    */
}
