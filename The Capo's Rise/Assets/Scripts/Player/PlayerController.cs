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
    private bool isShooting = false; // Variável para controlar o estado de tiro

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

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Move();
        HandleJump();
    }

    void Move()
    {
        if (isShooting) // Check if the character is shooting
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Stop horizontal movement
            anim.SetBool("IsRun", false); // Ensure running animation is off
            return;
        }

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        // Handle character orientation and running animation
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("IsRun", true);
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("IsRun", true);
        }
        else
        {
            anim.SetBool("IsRun", false);
        }
    }

    void HandleJump()
    {
        if (jumpRequested && !isShooting)
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
        isShooting = true; // O personagem está atirando
        anim.Play("Shoot", -1);
        StartCoroutine(ShootingCoroutine());
        anim.SetBool("IsRun", false);
    }

    IEnumerator ShootingCoroutine()
    {
        yield return new WaitForSeconds(0.2f); // Duração da animação de tiro
        isShooting = false; // O personagem terminou de atirar
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