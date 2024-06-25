using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e_bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private Vector3 direction; // 총알의 방향
    [SerializeField]private int timing = 0;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.up; // 이 스크립트가 총알에 추가되어 있고, 총알이 앞으로 나가기 때문에 up 방향을 사용합니다.
    }

    // Update is called once per frame
    void Update()
    {
        timing++;
        transform.position += direction * speed * Time.deltaTime;
        if (timing > 600)
        { // 시간 후 파괴
            Destroy(gameObject);
        }
        if(GAMEMANAGER.instance.game_start == false){
            Destroy(gameObject);
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    */
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
