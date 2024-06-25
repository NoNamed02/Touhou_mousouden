using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_bullet : MonoBehaviour
{
    [SerializeField] private int B_speed = 10;
    void Start()
    {
        AudioSource sound = GetComponent<AudioSource>();
        sound.Play();
    }

    void Update()
    {
        transform.Translate(Vector2.up * B_speed * Time.deltaTime, Space.Self);

        if (gameObject.transform.position.y >= 10)
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
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("boss"))
        {
            Destroy(gameObject);
        }
    }
}
