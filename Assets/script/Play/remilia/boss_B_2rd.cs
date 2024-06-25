using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_e_bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private Vector3 direction; // 총알의 방향
    [SerializeField]private int timing = 0;
    public int des_point = 1500;

    void Start()
    {
        direction = transform.up;
    }

    void Update()
    {
        if(GAMEMANAGER.instance.game_start == false)
            Destroy(gameObject);
        timing++;
        transform.position += direction * speed * Time.deltaTime;
        if (timing > des_point)
        { // 시간 후 파괴
            Destroy(gameObject);
        }
        if(GAMEMANAGER.instance.game_start == false)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("spellcard"))
        {
            Destroy(gameObject);
        }
    }
}
