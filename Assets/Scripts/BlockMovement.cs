using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour,IPooledObject
{
    [SerializeField] float speed = 3f;
    [SerializeField] float jumpForce;
    Rigidbody2D rb;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.velocity=new Vector2(0, speed);
    }
    public void OnObjectSpawn()
    {
        rb.velocity=new Vector2(0, speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        }
    }


}
