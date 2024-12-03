using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocidade = 2.0f;
    public float distanciaDeteccao = 5.0f;
    public GameObject jogador;
    public bool visualizarDeteccao = true;
    public Animator anim;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
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
