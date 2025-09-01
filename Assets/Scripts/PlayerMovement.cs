using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private Vector2 screenBounds;
    private float playerHalfWidth;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        playerHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        SpriteRenderer PlayerSprite = GetComponent<SpriteRenderer>();

        if (horizontalInput > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (horizontalInput < -0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && grounded)
            Jump();

        anim.SetBool("walk", horizontalInput != 0);


        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x + playerHalfWidth, screenBounds.x - playerHalfWidth);
        Vector2 pos = transform.position;
        pos.x = clampedX;
        transform.position = pos;
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

}
