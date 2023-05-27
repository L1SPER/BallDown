using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMovement : MonoBehaviour,IPooledObject
{
    private Rigidbody2D rb;
    [SerializeField] int point;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    public void OnObjectSpawn()
    {
        rb.velocity = Vector3.zero;
        return;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            ScoreManager.Instance.IncreaseTriangleScore(point);
        }
    }
}
