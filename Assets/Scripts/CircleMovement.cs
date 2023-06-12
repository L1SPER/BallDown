using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CircleMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D rb;
    private float ýnputX;
    [SerializeField] float touchScreenSpeedMultiplier;
    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = ýnputX;
        rb.velocity = velocity;
    }
    void Update()
    {
        Movement();
    }
    private void Movement()
    {   
        if(GameManager.Instance.currentGameMod== GameManager.GameMod.PhoneRotation)
            ýnputX = Input.acceleration.x*moveSpeed;
        else if(GameManager.Instance.currentGameMod == GameManager.GameMod.TouchScreen)
        {
            if(Input.touchCount>0)
            {
                Touch touch= Input.GetTouch(0);
                if(touch.phase==TouchPhase.Stationary)
                {
                    Vector3 screenPos = touch.position;
                    Vector3 worldPos=Camera.main.ScreenToWorldPoint(screenPos);
                    if (worldPos.x > 0)
                        ýnputX = touchScreenSpeedMultiplier * moveSpeed;
                    else if(worldPos.x<0)
                        ýnputX = -touchScreenSpeedMultiplier * moveSpeed;
                }
                else if(touch.phase==TouchPhase.Ended)
                {
                    ýnputX = 0f;
                }
            }
        }
        //ýnputX = Input.GetAxis("Horizontal") * moveSpeed;
        //transform.Translate(Input.acceleration.x, 0f,0f);
    }
}
