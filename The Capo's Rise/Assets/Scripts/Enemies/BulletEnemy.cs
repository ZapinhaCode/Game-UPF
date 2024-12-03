using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float Speed = 10f;           // Velocidade da bala
    public GameObject HurtSong;
    
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
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            playerController.life--;
            Destroy(gameObject);
            
            playerController.anim.SetTrigger("TakeDamage");
            
            if (HurtSong != null)
            {
                // Instancia o prefab de som na posição do jogador
                GameObject soundEffect = Instantiate(HurtSong, transform.position, Quaternion.identity);

                // Verifica se o prefab tem um AudioSource e toca o som
                AudioSource audioSource = soundEffect.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();

                    // Destroi o GameObject do som após a duração do áudio
                    Destroy(soundEffect, audioSource.clip.length);
                }
            }
        }
    }
}
