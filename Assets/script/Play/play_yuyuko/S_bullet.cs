using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_bullet : MonoBehaviour
{
    public float speed = 5f; // 총알의 이동 속도
    public float frequency = 1f; // S자 주파수
    public float magnitude = 0.5f; // S자 크기
    
    [SerializeField]private int timing = 0;
    public int des_point = 600;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        timing++;
        MoveInSShape();
        if (timing > des_point)
        { // 시간 후 파괴
            Destroy(gameObject);
        }
        if(GAMEMANAGER.instance.game_start == false)
            Destroy(gameObject);
    }

    void MoveInSShape()
    {
        float forwardMovement = speed * Time.deltaTime;

        float x = Mathf.Sin(Time.time * frequency) * magnitude;

        Vector3 newPosition = startPosition - transform.up * forwardMovement + transform.right * x;

        transform.position = newPosition;

        startPosition -= transform.up * forwardMovement;
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