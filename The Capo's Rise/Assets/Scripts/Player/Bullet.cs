using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10f;           // Velocidade da bala
    public int damage = 1;             // Dano que a bala causa

    void Start()
    {
        // Destroi a bala após 3 segundos para evitar acumulação
        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        // Move a bala na direção para a direita baseada na rotação atual
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.GetComponent<EnemyController>();
            enemyController.Life--;
            enemyController.anim.SetTrigger("TakeDamage");
            Destroy(gameObject);
        }
    }
}