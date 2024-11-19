using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX;
    private int currentJumps;
    private bool isGrounded;
    private bool jumpRequested;
    private Animator anim;

    public float speed;
    public float jumpForce;
    public int maxJumps = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        Move();
        HandleJump();
        Shoot();
    }

    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        // Verifica para trocar a posição do personagem 
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("IsRun", true);
        }

        if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("IsRun", true);
        }
        
        if (moveX == 0)
        {
            anim.SetBool("IsRun", false);
        }
    }

    void HandleJump()
    {
        if (jumpRequested)
        {
            if (isGrounded || (currentJumps > 0))
            {
                Jump();
                currentJumps--;
            }
            jumpRequested = false;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("IsJump", true);
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.Play("Shoot", -1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            currentJumps = maxJumps;
            anim.SetBool("IsJump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}