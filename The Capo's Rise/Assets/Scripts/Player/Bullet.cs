using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10f;           // Velocidade da bala
    public int damage = 20;             // Dano que a bala causa

    void Start()
    {
        // Destroi a bala após 3 segundos para evitar acumulação
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        // Move a bala na direção para a direita baseada na rotação atual
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    // Método chamado quando a bala entra em um gatilho (trigger)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido possui a tag "Enemy"
        if (collision.gameObject.tag == "Enemy")
        {
            // Tenta obter o componente EnemyHealth do inimigo
            EnemyController enemyHealth = collision.GetComponent<EnemyController>();
            if(enemyHealth != null)
            {
                // Aplica o dano ao inimigo
                enemyHealth.TakeDamage(damage);
            }

            // Destroi a bala após causar dano
            Destroy(gameObject);
        }
    }
}
