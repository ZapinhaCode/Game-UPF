using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public int damage = 10;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Verificar se colidiu com o jogador
        if (hitInfo.CompareTag("Player"))
        {
            // Implementar lógica de dano ao jogador
            // Por exemplo:
            // hitInfo.GetComponent<PlayerHealth>().TakeDamage(damage);
            
            Destroy(gameObject);
        }
        else
        {
            // Destruir o projétil ao colidir com outros objetos
            Destroy(gameObject);
        }
    }
}
