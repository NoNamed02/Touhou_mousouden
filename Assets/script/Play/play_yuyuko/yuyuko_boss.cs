using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yuyuko_boss : MonoBehaviour
{
    private int speed = 5;
    private int frameCounter = 0;
    private int framesPerAction = 500; // n프레임마다 실행
    public float HP = 3000;

    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public GameObject bulletPrefab4;

    public Transform point1;
    public Transform point2;
    public Transform point3;
    
    public GameObject HPBAR;
    
    public GameObject item;

    public Transform bulletSpawnPoint;
    //public Transform Point1;
    //public Transform Point2;

    public txt_remilia_play txtmanager;
    public talk_bg talk_Bg;
    private Animator animator;
    private int hit_count = 0;

    public Player_move_reimu player;
    // Start is called before the first frame update
    void Start()
    {
        frameCounter = 300;
        HPBAR.SetActive(true);
        animator = GetComponent<Animator>();
        BGMmanager.Instance.Playsound("yuyuko_boss_play");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;

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
            txtmanager.currentLine = 18;
            talk_Bg.gameObject.SetActive(true);
            talk_Bg.back_st();
            txtmanager.DisplayNextSentence();
            hasExecuted = true;
        }
        if (GAMEMANAGER.instance.game_start)
            frameCounter++;

        if (frameCounter >= framesPerAction && GAMEMANAGER.instance.game_start)
        { //n프레임 마다 한번
            int rand_n = Random.Range(1, 5);
            if (rand_n == 1)
            {
                StartCoroutine(FireCircleBullets());
                StartCoroutine(FireBullets());
            }
            else if (rand_n == 2)
            {
                StartCoroutine(FireBullets());
            }
            else if (rand_n == 3)
            {
                StartCoroutine(CircleBullets());
            }
            else if (rand_n == 4)
            {
                StartCoroutine(S_bullet_fire());
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
            yield return new WaitForSeconds(15f);

            float[] positions = { -7.5f, -4.5f, -1.5f };
            float newX = positions[Random.Range(0, positions.Length)];

            Vector3 startPosition = transform.position;
            Vector3 targetPosition = new Vector3(newX, transform.position.y, transform.position.z);
            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float startTime = Time.time;

            if (targetPosition.x > startPosition.x)
            {
                animator.SetInteger("ani", 1); // 오른쪽
            }
            else if (targetPosition.x < startPosition.x)
            {
                animator.SetInteger("ani", -1); // 왼쪽
            }
            else
            {
                animator.SetInteger("ani", 0); // 멈춤
            }

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

            // 멈추면 애니메이션 설정
            animator.SetInteger("ani", 0);
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
        for (float i = 0f; i < 200f; i++)
        {
            float sub = Random.Range(10, -10);
            float angle = 0 + (18f * i);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle + sub);
            Instantiate(bulletPrefab3, point1.position, rotation);
            Instantiate(bulletPrefab3, point2.position, rotation);
            Instantiate(bulletPrefab3, point3.position, rotation);
            yield return new WaitForSeconds(0.1f); // 0.03초 간격으로 발사
        }
    }

    IEnumerator S_bullet_fire(){
        for (float i = 0f; i < 400f; i++)
        {
            float angle = 0 + (30f * i);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab4, bulletSpawnPoint.position, rotation);
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator rest()
    {
        yield return new WaitForSeconds(5.0f); // 3초 그로기
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player_bullet"))
        {
            hit_count++;
            if (hit_count == 2)
            {
                player.level++;
                hit_count = 0;
            }
            HP -= 1;
            GAMEMANAGER.instance.score += 20;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("spellcard") && GAMEMANAGER.instance.player_is_reimu == false)
        {
            HP -= 0.01f;
            GAMEMANAGER.instance.score += 1;
        }
    }
}
