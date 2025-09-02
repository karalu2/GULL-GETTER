using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    // [SerializeField] private Transform fishPile;
    private Transform stash;
    [SerializeField] private float speed;
    [SerializeField] private int range;
    [SerializeField] private float health, maxHealth = 3f;
    [SerializeField] private float stopDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask stashLayer;

    [SerializeField] EnemyHealthBar healthBar;

    private StashBehavior stashBehavior;
    private HealthBar stashHealth;

    [SerializeField] float eatingTime = 6f;
    [SerializeField] float baseDamage = 5f;
    private float startTime;
    
    private Animator anim;

    private void Start()
    {
        stash = GameObject.FindGameObjectWithTag("Stash").transform;
        healthBar.UpdateHealthBar(health, maxHealth);

        stashBehavior = GameObject.FindGameObjectWithTag("Stash").GetComponent<StashBehavior>();
        stashHealth = GameObject.FindGameObjectWithTag("Stash HP").GetComponent<HealthBar>();

        startTime = Time.time;
    }

    private void Update()
    {
        if (FishInRange())
        {
            Debug.Log("eating");
            anim.SetBool("eat", true);
            MoveInDirection(0);

            if (Time.time - startTime >= eatingTime)
            {
                stashBehavior.TakeDamage(baseDamage);
                Destroy(gameObject);
            }

            return;
        }

        anim.SetBool("eat", false);

        if (transform.position.x > (stash.position.x + stopDistance))
        {
            MoveInDirection(-1);
        }
        else if (transform.position.x < (stash.position.x - stopDistance))
        {
            MoveInDirection(1);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void MoveInDirection(int _direction)
    {
        if (_direction >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }

    private bool FishInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * range, boxCollider.bounds.size.z),
            0, Vector2.left, 0, stashLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * range, boxCollider.bounds.size.z));
    }
}
