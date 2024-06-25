using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remilia : MonoBehaviour
{
    private int speed = 5;
    private int frameCounter = 0;
    private int framesPerAction = 500; // n프레임마다 실행
    public float HP = 3000;

    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;

    public Transform bulletSpawnPoint;
    public Transform Point1;
    public Transform Point2;

    public txt_remilia_play txtmanager;
    public talk_bg talk_Bg;
    public GameObject reimu;
    public GameObject marisa;
    public GameObject remil;
    public Player_move_reimu player;
    private int hit_count = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;

        // 새로운 코루틴 시작
        StartCoroutine(MovePosition());
    }
    private bool hasExecuted;
    // Update is called once per frame
    void Update()
    {
        if (HP < 0)
        {
            HP = 0;
            GAMEMANAGER.instance.game_start = false;
        }
        if (!hasExecuted && HP <= 0)
        {
            txtmanager.currentLine = 15;
            talk_Bg.gameObject.SetActive(true);
            talk_Bg.back_st();
            txtmanager.DisplayNextSentence();
            reimu.SetActive(true);
            marisa.SetActive(true);
            remil.SetActive(true);
            hasExecuted = true;
        }
        if (GAMEMANAGER.instance.game_start)
            frameCounter++;
        if (frameCounter >= framesPerAction)
        { //n프레임 마다 한번
            int rand_n = Random.Range(1, 4);
            if (rand_n == 1)
            {
                StartCoroutine(FireCircleBullets());
                StartCoroutine(FireBullets());
            }
            else if (rand_n == 2)
            {
                StartCoroutine(FireBullets());
                StartCoroutine(CircleBullets());
            }
            else if (rand_n == 3)
            {
                StartCoroutine(CircleBullets());
                StartCoroutine(FireCircleBullets());
            }
            frameCounter = 0;
        }
        if (gameObject.transform.position.y > 4)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        StartCoroutine(rest());
    }

    IEnumerator MovePosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // 10초마다

            float[] positions = { -7.5f, -4.5f, -1.5f };
            float newX = positions[Random.Range(0, positions.Length)];

            // 새로운 위치로 부드럽게 이동
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = new Vector3(newX, transform.position.y, transform.position.z);
            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float startTime = Time.time;
            //float journeyTime = 2.0f; // 이동에 걸리는 시간 (초)

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                if (GAMEMANAGER.instance.game_start)
                {
                    float distCovered = (Time.time - startTime) * speed;
                    float fractionOfJourney = distCovered / journeyLength;
                    transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
                }
                yield return null;
            }
        }
    }

    IEnumerator FireCircleBullets()
    {
        for (int j = 0; j < 10; j++) // n번 반복
        {
            for (float i = 0f; i < 20f; i++)
            {
                float angle = 0 + (18f * i);
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Instantiate(bulletPrefab, bulletSpawnPoint.position, rotation);
            }
            yield return new WaitForSeconds(0.3f);
            for (float i = 0f; i < 20f; i++)
            {
                float angle = 9f + (18f * i);
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Instantiate(bulletPrefab, bulletSpawnPoint.position, rotation);
            }
            yield return new WaitForSeconds(0.5f); // 0.5초 간격으로 발사
        }
        yield return new WaitForSeconds(0.5f); // 0.5초 추가 대기
    }

    IEnumerator FireBullets() // 유도 칼날
    {
        for (int i = 0; i < 30; i++)
        {
            float angle = Random.Range(0, 360); // 무작위 각도 생성
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle); // 회전값 생성
            Vector3 direction = rotation * Vector3.up; // 총알의 이동 방향 계산

            Instantiate(bulletPrefab2, bulletSpawnPoint.position, rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator CircleBullets() // 나선 탄막
    {
        for (float i = 0f; i < 400f; i++)
        {
            float sub = Random.Range(10, -10);
            float angle = 0 + (18f * i);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle + sub);
            Instantiate(bulletPrefab3, bulletSpawnPoint.position, rotation);
            yield return new WaitForSeconds(0.03f); // 0.03초 간격으로 발사
        }
    }

    IEnumerator rest()
    {
        yield return new WaitForSeconds(3.0f); // 3초 그로기
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player_bullet"))
        {
            HP -= 1;
            GAMEMANAGER.instance.score += 20;
            hit_count++;
            if (hit_count == 2)
            {
                player.level++;
                hit_count = 0;
            }
            HP -= 1;
            GAMEMANAGER.instance.score += 20;
        }
        /*
        if (collision.gameObject.CompareTag("spellcard") && GAMEMANAGER.instance.player_is_reimu == false)
        {
            HP -= 0.01f;
            GAMEMANAGER.instance.score += 1;
        }
        */
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("spellcard") && GAMEMANAGER.instance.player_is_reimu == false)
        {
            HP -= 0.1f;
            GAMEMANAGER.instance.score += 1;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player_bullet") && is_hit == false)
        {
            anim.SetInteger("is_hit_ani",1);
            GAMEMANAGER.instance.score += 100;
            GAMEMANAGER.instance.enemy_break_count += 1;
            StartCoroutine(die());
            is_hit = true;
        }
        if (collision.gameObject.CompareTag("spellcard"))
        {
            AudioSource sound = GetComponent<AudioSource>();
            sound.Play();
            Destroy(gameObject);
        }
    }
    */
}
