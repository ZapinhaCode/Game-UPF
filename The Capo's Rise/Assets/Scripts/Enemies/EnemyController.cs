using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 2.0f;
    public float distanciaDeteccao = 5.0f;

    [Header("Referência ao Jogador")]
    public GameObject jogador;

    [Header("Visualização")]
    public bool visualizarDeteccao = true;

    [Header("Referência ao Animator")]
    public Animator anim;

    [Header("Referência ao SpriteRenderer")]
    public SpriteRenderer spriteRenderer;
    
    [Header("Configurações de Disparo")]
    public Transform firePoint;
    public GameObject bullet;
    public float tempoEntreDisparos = 2.0f; // Tempo em segundos entre os disparos
    private float ultimoDisparo = 0f;

    [Header("Configurações de Vida")]
    public int life;

    public GameObject GunShootSongEnemyPrefab;
    public GameObject deathSoundPrefab;

    private Rigidbody2D rb;

    void Start()
    {
        anim.SetBool("IsRun", false);
        anim.Play("Idle", -1);
        rb = GetComponent<Rigidbody2D>();

        // Verifica se o Animator está atribuído
        if (anim == null)
        {
            anim = GetComponent<Animator>();
            if (anim == null)
            {
                Debug.LogError("Animator não encontrado no inimigo. Certifique-se de que o componente Animator está anexado.");
            }
        }

        // Verifica se o SpriteRenderer está atribuído
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer não encontrado no inimigo. Certifique-se de que o componente SpriteRenderer está anexado.");
            }
        }

        // Encontra o jogador pela tag se não estiver atribuído manualmente
        if (jogador == null)
        {
            GameObject jogadorEncontrado = GameObject.FindGameObjectWithTag("Player");
            if (jogadorEncontrado != null)
            {
                jogador = jogadorEncontrado;
            }
            else
            {
                Debug.LogError("Jogador não encontrado na cena. Certifique-se de que o jogador tem a tag 'Player' ou atribua-o manualmente.");
            }
        }
    }

    void FixedUpdate()
    {
        if (jogador != null)
        {
            float distancia = Vector2.Distance(transform.position, jogador.transform.position);
            if (distancia <= distanciaDeteccao)
            {
                Vector2 direcao = (jogador.transform.position - transform.position).normalized;
                rb.velocity = direcao * velocidade;

                // Ativa a animação de corrida
                if (anim != null)
                {
                    anim.SetBool("IsRun", true);
                }

                // Vira o inimigo na direção do jogador
                VirarParaJogador(jogador.transform.position);

                // Chama o método de atirar
                Shot();
            }
            else
            {
                rb.velocity = Vector2.zero;

                // Desativa a animação de corrida
                if (anim != null)
                {
                    anim.SetBool("IsRun", false);
                    // Para a animação de tiro
                    StopShot();
                }
            }
        }
        else
        {
            // Desativa a animação de corrida se o jogador não estiver referenciado
            if (anim != null)
            {
                anim.SetBool("IsRun", false);
                // Para a animação de tiro
                StopShot();
            }
        }
    }

    /// <summary>
    /// Vira o inimigo na direção do jogador.
    /// </summary>
    /// <param name="posicaoJogador">Posição do jogador no mundo.</param>
    void VirarParaJogador(Vector3 posicaoJogador)
    {
        if (spriteRenderer == null) return;

        // Determina se o jogador está à direita ou à esquerda do inimigo
        if (posicaoJogador.x > transform.position.x && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
        else if (posicaoJogador.x < transform.position.x && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
    }

    void Shot()
    {
        if (Time.time >= ultimoDisparo + tempoEntreDisparos)
        {
            if (anim != null)
            {
                anim.Play("Shot", -1);
                Invoke("StopShot", 0.5f); // Ajuste o tempo conforme a duração da animação
                
                if (GunShootSongEnemyPrefab != null)
                {
                    // Instancia o prefab de som na posição do jogador
                    GameObject soundEffect = Instantiate(GunShootSongEnemyPrefab, transform.position, Quaternion.identity);
            
                    // Verifica se o prefab tem um AudioSource e toca o som
                    AudioSource audioSource = soundEffect.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.Play();
                    }

                    // Destroi o GameObject do som após a duração do áudio
                    Destroy(soundEffect, audioSource != null ? audioSource.clip.length : 1f);
                }
            }

            // Instanciar a bala
            if (firePoint != null && bullet != null && jogador != null)
            {
                // Calcular a direção para o jogador
                Vector3 direcao = (jogador.transform.position - firePoint.position).normalized;

                // Calcular o ângulo para a rotação da bala
                float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                Quaternion rotacao = Quaternion.Euler(0, 0, angulo);

                // Instanciar a bala com a rotação calculada
                GameObject bulletInstance = Instantiate(bullet, firePoint.position, rotacao);

                // Definir a velocidade da bala
                Rigidbody2D rbBullet = bulletInstance.GetComponent<Rigidbody2D>();
                if (rbBullet != null)
                {
                    float bulletSpeed = 10f; // Você pode ajustar a velocidade conforme necessário
                    rbBullet.velocity = direcao * bulletSpeed;
                }
                else
                {
                    Debug.LogError("O prefab da bala não possui um componente Rigidbody2D.");
                }
            }

            ultimoDisparo = Time.time;
        }
    }

    void StopShot()
    {
        if (anim != null)
        {
            anim.SetBool("IsFire", false);
            anim.SetBool("IsRun", true);
        }
    }
    void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("BulletPlayer"))
        {
            anim.SetBool("IsRun", false);
            anim.SetTrigger("TakeDamage");
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
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
        
        life -= 1;
        if (life <= 0)
        {
            Die();
        }
    }

    void Die()
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
        
        Destroy(gameObject, 2.1f);
    }

    void OnDrawGizmosSelected()
    {
        if (visualizarDeteccao)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanciaDeteccao);
        }
    }
}
