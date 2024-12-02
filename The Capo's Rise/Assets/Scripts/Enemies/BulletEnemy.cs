using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public int damage = 1; // Dano que o projetil causa
    public float lifetime = 5f; // Tempo de vida do projetil
    public float speed = 10f; // Velocidade do projetil

    void Start()
    {
        // Destruir o projetil após o tempo de vida
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Movimentar o projetil na direção atual
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o projetil colidiu com o jogador
        if (collision.CompareTag("Player"))
        {
            // Aplicar dano ao jogador
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.life -= damage;
            }

            // Destruir o projetil após colidir com o jogador
            Destroy(gameObject);
        }

        // Opcional: Destruir o projetil ao colidir com o chão ou outros objetos
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
