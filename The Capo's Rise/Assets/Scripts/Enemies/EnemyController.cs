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

    private Rigidbody2D rb;

    void Start()
    {
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
            }
            else
            {
                rb.velocity = Vector2.zero;

                // Desativa a animação de corrida
                if (anim != null)
                {
                    anim.SetBool("IsRun", false);
                }
            }
        }
        else
        {
            // Desativa a animação de corrida se o jogador não estiver referenciado
            if (anim != null)
            {
                anim.SetBool("IsRun", false);
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

    void OnDrawGizmosSelected()
    {
        if (visualizarDeteccao)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanciaDeteccao);
        }
    }
}
