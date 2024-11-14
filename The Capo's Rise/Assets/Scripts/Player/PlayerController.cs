using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // VARIAVEIS PRIVADAS
    private Rigidbody2D rb;
    private float moveX;

    // VARIAVEIS PUBLICAS
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveX = Input.GetAxisRaw("Horizontal");
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
    }
}
