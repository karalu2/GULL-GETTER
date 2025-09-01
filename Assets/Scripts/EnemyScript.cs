using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private int greed;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask fishLayer;

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (FishInRange())
        {
            if (cooldownTimer >= attackCoolDown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("eat");
            }
        }        
    }

    private bool FishInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, fishLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
