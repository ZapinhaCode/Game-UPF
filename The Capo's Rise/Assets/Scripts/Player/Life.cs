using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu não é o Player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            playerController.life++;
            Destroy(gameObject);
        }
    }
}
