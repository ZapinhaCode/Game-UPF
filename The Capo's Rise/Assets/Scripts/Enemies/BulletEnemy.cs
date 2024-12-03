using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float velocidade = 15f; // Velocidade aumentada para balas mais rápidas
    public float vidaUtil = 3f; // Tempo de vida do projétil em segundos

    private Vector2 direcao;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no projétil.");
        }
    }

    // Inicializa o projétil com uma direção
    public void Inicializar(Vector2 _direcao)
    {
        direcao = _direcao.normalized;

        // Orienta o projétil na direção do movimento
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));

        // Define a velocidade do Rigidbody2D
        if (rb != null)
        {
            rb.velocity = direcao * velocidade;
        }
    }

    void Update()
    {
        // Diminui a vida útil
        vidaUtil -= Time.deltaTime;
        if (vidaUtil <= 0)
        {
            RetornarAoPool();
        }
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        // Implementar lógica de dano ao jogador ou outros objetos
        if (colisao.CompareTag("Player"))
        {
            // Exemplo: Diminui a vida do jogador
            // colisao.GetComponent<PlayerHealth>().Dano(1);
            Debug.Log("Jogador atingido pelo projétil!");
            RetornarAoPool();
        }

        // Destruir o projétil ao colidir com qualquer coisa, exceto inimigos
        if (!colisao.CompareTag("Enemy"))
        {
            RetornarAoPool();
        }
    }

    void RetornarAoPool()
    {
        // Reseta a vida útil e a velocidade
        vidaUtil = 3f;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
