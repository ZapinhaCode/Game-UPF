using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 3f; // Velocidade de movimento do inimigo

    [Header("Ataque")]
    public float fireRate = 1f; // Frequência de disparo (disparos por segundo)
    public GameObject bulletPrefab; // Prefab do projétil
    public Transform firePoint; // Ponto de onde o projétil será disparado

    [Header("Detecção")]
    public float detectionRadius = 10f; // Raio de detecção do jogador
    public LayerMask playerLayer; // Camada do jogador

    [Header("Animações")]
    private Animator animator;

    private Transform player;
    private float nextFireTime = 0f;
    private bool playerDetected = false;

    void Start()
    {
        // Inicializar o Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator não encontrado no inimigo.");
        }

        // Encontrar o jogador na cena
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player não encontrado na cena. Certifique-se de que o Player tenha a tag 'Player'.");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // Verificar se o jogador está dentro do raio de detecção
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        // Atualizar as animações
        UpdateAnimations();

        if (playerDetected)
        {
            // Movimentar em direção ao jogador
            MoveTowardsPlayer();

            // Verificar se é hora de atirar
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            // Opcional: Comportamento quando o jogador não está no alcance
            // Por exemplo, patrulhar ou ficar parado
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (animator != null)
            {
                animator.SetTrigger("IsFire");
            }
        }
        else
        {
            Debug.LogWarning("BulletPrefab ou FirePoint não está atribuído.");
        }
    }

    void UpdateAnimations()
    {
        if (animator != null)
        {
            if (playerDetected)
            {
                animator.SetBool("IsRun", true);
            }
            else
            {
                animator.SetBool("IsRun", false);
            }
            // A animação de atirar já é acionada no método Shoot()
        }
    }

    // Opcional: Visualizar a área de detecção no Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
