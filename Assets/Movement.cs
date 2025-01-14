using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 25f;
    public float dashForce = 10f;
    private bool TouchFloor = true;
    private Rigidbody2D rb;
    int jump_count = 0;

    bool IsDashing = false;
    public float Dash_cooldown = 1f;
    public float Last_used_time;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        Dash();
    }

    void Move()
    {
        if (!IsDashing)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TouchFloor)
        {
            jump_count += 1;
            if (jump_count == 2)
            {
                TouchFloor = false;
            }
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Dash()
    {
        float dashDirection = Input.GetAxisRaw("Horizontal");

         if(Time.time > Last_used_time +  Dash_cooldown)
            {
                IsDashing = false;
            }


        if (Input.GetKeyDown(KeyCode.Q) && dashDirection != 0 && IsDashing == false)
        {
            print("Triggered Dash");
            IsDashing = true;
            Last_used_time = Time.time;
            rb.linearVelocity = new Vector2(dashDirection * dashForce, rb.linearVelocity.y);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            TouchFloor = true;
            jump_count = 0;
        }
    }
}