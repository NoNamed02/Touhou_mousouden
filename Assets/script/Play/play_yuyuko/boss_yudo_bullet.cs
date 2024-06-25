using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_yudo_bullet_ : MonoBehaviour
{
    private Vector3 playerDirection; // 플레이어 방향을 저장할 변수
    [SerializeField] private float speed1 = 1.0f;
    [SerializeField] private float speed2 = 8.0f;
    [SerializeField] private int timing = 0;

    private bool ro_check = false;

    private int time_check = 0;
    
    private int time_check_out = 70;
    //private bool isMovingTowardsPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if(GAMEMANAGER.instance.game_start == false)
            Destroy(gameObject);
        timing++;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        time_check++;
        time_check_out = Random.Range(150, 280);
        if (time_check < time_check_out)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed1);
        }
        else if (time_check < 300 && time_check >= time_check_out)
        {
            // 500프레임 이상, 1000프레임 미만일 때, 플레이어를 향해 회전하고 멈춤
            if (playerObject != null)
            {
                playerDirection = (playerObject.transform.position - transform.position).normalized;
                RotateToFaceDirection(playerDirection); // 플레이어를 향해 회전
                //isMovingTowardsPlayer = true; // 플레이어를 향해 이동 중임을 표시
            }
            else
            {
                Destroy(gameObject); // 플레이어를 찾지 못한 경우 총알 파괴
            }
        }
        else
        {
            if(ro_check == false){
                if (playerObject != null)
                {
                    playerDirection = (playerObject.transform.position - transform.position).normalized;
                }
            }
            transform.Translate(Vector3.up * Time.deltaTime * speed2);
        }

        if (timing > 1000)
        { // 거리 벌어지면 파괴
            Destroy(gameObject);
        }
        if(GAMEMANAGER.instance.game_start == false)
            Destroy(gameObject);
    }

    // 이동 방향에 따라 객체를 회전시키는 함수
    private void RotateToFaceDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            // 플레이어를 향하는 방향을 구하고, 그 방향으로 회전
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // 이동 방향도 설정
            playerDirection = direction;
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
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
