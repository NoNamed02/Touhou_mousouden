using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_bullet_2rd : MonoBehaviour
{
    [SerializeField] private int B_speed = 10;
    
    [SerializeField] private int des_point = 7;
    

    void Update()
    {
        transform.Translate(Vector2.up * B_speed * Time.deltaTime, Space.Self);

        if (gameObject.transform.position.y >= des_point)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("boss"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("boss"))
        {
            Destroy(gameObject);
        }
    }
}
