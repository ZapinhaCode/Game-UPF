using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public PlayerController heart;
    public PlayerController player;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            heart.life--;
            if (player != null)
            {
                player.anim.SetTrigger("TakeDamage");
            }
        }
    }
}