using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Componentes
    private Rigidbody2D rb;
    private Animator anim;

    // Referências
    public Transform player; // Referência ao jogador
    public GameObject bulletPrefab; // Prefab do projétil
    public Transform firePoint; // Ponto de onde os projéteis serão instanciados

    // Parâmetros de IA
    [Header("Detecção")]
    public float detectionRange = 10f; // Alcance de detecção do jogador
    public float attackRange = 8f; // Alcance para atacar
    public float shootingCooldown = 2f; // Tempo entre os tiros
    private float lastShotTime = 0f;

    [Header("Vida")]
    public int maxLife = 1; // Vida máxima do inimigo
    private int currentLife;

    [Header("Movimentação")]
    public float speed = 2f; // Velocidade de movimentação
    public float patrolDistance = 5f; // Distância para patrulhar
    private Vector3 patrolStartPoint;
    private bool movingRight = true;

    [Header("Pular (Opcional)")]
    public bool canJump = false;
    public float jumpForce = 5f;
    public LayerMask groundLayer; // Camada que representa o chão
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Animações e Sons")]
    public Animator enemyAnimator;
    public GameObject deathSoundPrefab;
    public GameObject shootSoundPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentLife = maxLife;
        patrolStartPoint = transform.position;

        // Encontrar o jogador por tag caso não esteja atribuído
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        // Verifica a distância até o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            // Se o jogador está dentro do alcance de detecção mas fora do alcance de ataque, patrulha
            Patrol();
        }
        else if (distanceToPlayer <= attackRange)
        {
            // Se o jogador está dentro do alcance de ataque, ataca
            AttackPlayer();
        }
        else
        {
            // Se o jogador está fora do alcance de detecção, patrulha
            Patrol();
        }

        // Atualiza animações com base no estado
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            anim.SetBool("IsGrounded", IsGrounded());
        }
    }

    void Patrol()
    {
        // Patrulha para a direita e para a esquerda dentro do patrolDistance
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x >= patrolStartPoint.x + patrolDistance)
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x <= patrolStartPoint.x - patrolDistance)
            {
                Flip();
            }
        }

        // Opcional: Pular enquanto patrulha
        if (canJump && IsGrounded())
        {
            Jump();
        }
    }

    void AttackPlayer()
    {
        // Parar de patrulhar e enfrentar o jogador
        rb.velocity = new Vector2(0, rb.velocity.y);
        FacePlayer();

        // Atirar se o cooldown permitir
        if (Time.time > lastShotTime + shootingCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }

        // Opcional: Pular para alcançar o jogador
        if (canJump && IsGrounded())
        {
            Jump();
        }
    }

    void FacePlayer()
    {
        if (player.position.x > transform.position.x && !movingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && movingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scaler = transform.localScale;
        scaler.y *= 1; // Mantém a escala Y
        scaler.x *= -1; // Inverte a escala X para virar o inimigo
        transform.localScale = scaler;
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Tocar som de atirar
            if (shootSoundPrefab != null)
            {
                GameObject soundEffect = Instantiate(shootSoundPrefab, transform.position, Quaternion.identity);
                AudioSource audioSource = soundEffect.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                    Destroy(soundEffect, audioSource.clip.length);
                }
            }

            // Atualizar animação de atirar
            if (anim != null)
            {
                anim.SetTrigger("Shoot");
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (anim != null)
        {
            anim.SetTrigger("Jump");
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Método para receber dano
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        if (anim != null)
        {
            anim.SetTrigger("Hurt");
        }

        if (currentLife <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Desativar o collider e Rigidbody para evitar interações adicionais
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        // Tocar animação de morte
        if (anim != null)
        {
            anim.SetBool("IsDead", true);
        }

        // Tocar som de morte
        if (deathSoundPrefab != null)
        {
            GameObject soundEffect = Instantiate(deathSoundPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = soundEffect.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
                Destroy(soundEffect, audioSource.clip.length);
            }
        }

        // Destruir o GameObject após a animação
        Destroy(gameObject, 2f); // Ajuste o tempo conforme a duração da animação
    }

    // Visualização do groundCheck no editor
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
