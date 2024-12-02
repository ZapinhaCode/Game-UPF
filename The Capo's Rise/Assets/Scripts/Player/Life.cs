using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public GameObject audioHeartPrefab;
    public int scoreLife;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();

            // Aumenta a vida do jogador
            playerController.life++;
            scoreLife = playerController.life;

            // Pega a posição do Player
            Vector3 playerPosition = collision.transform.position;

            // Instancia o prefab de áudio na posição do jogador
            GameObject prefab = Instantiate(audioHeartPrefab, playerPosition, Quaternion.identity);

            // Toca o som
            AudioSource audioSource = prefab.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }

            Destroy(prefab.gameObject, 1.5f);
            Destroy(gameObject);
        }
    }
}