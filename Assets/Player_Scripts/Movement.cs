using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 25f;
    public float dashForce = 10f;
    private bool TouchFloor = true;
    private bool FaceRight = true;
    private Rigidbody2D rb;
    int jump_count = 0;
    bool IsDashing = false;
    bool CanDash = true;
    public float DashingTime = 0.4f;
    public float Dash_cooldown = 1f;
    public float Last_used_time;
    public TrailRenderer tr;
    public GameObject Pinceau;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        ChangeSide();
        animCheck();
        if (Input.GetKeyDown(KeyCode.Q) && CanDash)
        {
            StartCoroutine(Dash());
        }
 
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

    private IEnumerator Dash()
    {
        CanDash = false;
        IsDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashForce, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(DashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        IsDashing = false;
        yield return new WaitForSeconds(Dash_cooldown);
        CanDash = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            TouchFloor = true;
            jump_count = 0;
        }
    }

    private void ChangeSide()
    {
        if (FaceRight && Input.GetAxisRaw("Horizontal") < 0f || !FaceRight && Input.GetAxisRaw("Horizontal") > 0f)
        {
            FaceRight = !FaceRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            Pinceau.transform.localScale *= -1f;
            transform.localScale = localScale;

        }
    }
    private void animCheck()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.linearVelocityX));
        anim.SetFloat("velocityX", rb.linearVelocityY);
        anim.SetBool("grounded", TouchFloor);
    }
}