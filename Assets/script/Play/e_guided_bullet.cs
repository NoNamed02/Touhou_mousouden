using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e_guided_bullet : MonoBehaviour
{
    private Vector3 playerDirection; // 플레이어 방향을 저장할 변수
    [SerializeField]private float speed = 3.0f;
    [SerializeField]private int timing = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerDirection = (playerObject.transform.position - transform.position).normalized;
        }
    }

    // Update is called once per framea
    void Update()
    {
        timing++;
        transform.Translate(playerDirection * speed * Time.deltaTime);
        if (timing > 1000)
        { // 거리 벌어지면 파괴
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
