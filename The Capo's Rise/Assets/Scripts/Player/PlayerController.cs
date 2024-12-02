using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX;
    private int currentJumps;
    private bool isGrounded;
    private bool jumpRequested;
    private bool isShooting = false;

    public float speed;
    public float jumpForce;
    public int maxJumps = 1;
    public GameObject bullet;
    public Transform firePoint;
    public int life;
    public TextMeshProUGUI textLife;
    public Animator anim;
    public GameObject gameOverCanvas;
    public GameObject deathSoundPrefab;    
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
        
        textLife.text = life.ToString();
        
        DeadState(deathSoundPrefab);
    }

    void FixedUpdate()
    {
        Move();
        HandleJump();
    }

    void Move()
    {
        if (isShooting)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("IsRun", false);
            return;
        }

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

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
        isShooting = true;
        anim.Play("Shoot", -1);
        StartCoroutine(ShootingCoroutine());
        anim.SetBool("IsRun", false);
        
        Instantiate(bullet,firePoint.transform.position, firePoint.transform.rotation );
    }

    IEnumerator ShootingCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        isShooting = false;
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
    
    void DeadState(GameObject deathSoundPrefab)
    {
        if (life <= 0)
        {
            enabled = false; // Desativa o PlayerController
            anim.SetBool("IsDie", true);
            
            // Toca o som manualmente, verificando se o prefab de som contém um AudioSource
            if (deathSoundPrefab != null)
            {
                // Instancia o prefab de som na posição do jogador
                GameObject soundEffect = Instantiate(deathSoundPrefab, transform.position, Quaternion.identity);
            
                // Verifica se o prefab tem um AudioSource e toca o som
                AudioSource audioSource = soundEffect.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                }

                // Destroi o GameObject do som após a duração do áudio
                Destroy(soundEffect, audioSource != null ? audioSource.clip.length : 1f);
            }
            
            StartCoroutine(ShowGameOverAfterDelay(1.7f));
        }
    }

    IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Aguarda o tempo especificado

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }
}