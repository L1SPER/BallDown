using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
            GameManager.Instance.ShowCanvas("GameOverCanvas");
        }
        else if(collision.gameObject.name== "FirstBlock")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
