using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public StashBehavior stash;
    [SerializeField] HealthBar healthBar;
    [SerializeField] float itemValue = 5;
    private string itemTag = "Item";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(itemTag))
        {
            stash.health = Mathf.Min(healthBar.GetMaxHealth(), stash.health + itemValue);
            healthBar.SetHealth(stash.health);
            Destroy(collision.gameObject);
            //Debug.Log($"Item picked up at {collision.gameObject.transform.position}");
        }     
    }
}
