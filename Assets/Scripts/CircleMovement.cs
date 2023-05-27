using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CircleMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D rb;
    private float �nputX;
    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = �nputX;
        rb.velocity = velocity;
    }
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        �nputX = Input.acceleration.x*moveSpeed;
        //�nputX = Input.GetAxis("Horizontal")*moveSpeed;
        //transform.Translate(Input.acceleration.x, 0f,0f);
    }
}
