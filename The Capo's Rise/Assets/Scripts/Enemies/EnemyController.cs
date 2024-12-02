using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 3;

    // Método para aplicar dano
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Método para destruir o inimigo
    void Die()
    {
        Destroy(gameObject);
    }
}
