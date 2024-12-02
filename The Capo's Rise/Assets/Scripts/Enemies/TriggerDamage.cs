using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public PlayerController heart;
    public PlayerController player;
    public GameObject HurtSongPrefab;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            heart.life--;
            if (player != null)
            {
                player.anim.SetTrigger("TakeDamage");
                
                if (HurtSongPrefab != null)
                {
                    // Instancia o prefab de som na posição do jogador
                    GameObject soundEffect = Instantiate(HurtSongPrefab, transform.position, Quaternion.identity);
            
                    // Verifica se o prefab tem um AudioSource e toca o som
                    AudioSource audioSource = soundEffect.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.Play();
                    }

                    // Destroi o GameObject do som após a duração do áudio
                    Destroy(soundEffect, audioSource != null ? audioSource.clip.length : 1f);
                }
            }
        }
    }
}