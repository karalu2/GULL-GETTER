using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (horizontalInput > 0.01f)
            sr.flipX = false;
        else if (horizontalInput < -0.01f)
            sr.flipX = true;
    }

}
