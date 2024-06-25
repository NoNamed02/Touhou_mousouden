using System.Collections;
using UnityEngine;

public class yukari_boss : MonoBehaviour
{
    [SerializeField]private int speed = 2;
    private int frameCounter = 0;
    private int framesPerAction = 500; // n프레임마다 실행
    public float HP = 1000;
    public int phase = 0; // 페이즈

    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public GameObject bulletPrefab4;
    public GameObject bulletPrefab5;

    public Transform point1;
    public Transform point2;
    public Transform point3;

    public GameObject HPBAR;

    public GameObject item;

    public Transform bulletSpawnPoint;

    public yukari_txt txtmanager;
    public talk_bg talk_Bg;
    private Animator animator;
    private int hit_count = 0;

    public Player_move_reimu player;

    private bool isRecovering = false; // 체력 회복 중인지 여부

    // Start is called before the first frame update
    void Start()
    {
        frameCounter = 300;
        HPBAR.SetActive(true);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;

        StartCoroutine(MovePosition());
    }

    private bool hasExecuted = false;
    int rand_n = 0;

    void Update()
    {
        if (HP <= 0 && !isRecovering && phase < 4)
        {
            phase++;
            StartCoroutine(RecoverHealth());
        }

        if (phase == 3 && hasExecuted != true)
        {
            GAMEMANAGER.instance.game_start = false;
            txtmanager.currentLine = 24;
            talk_Bg.gameObject.SetActive(true);
            talk_Bg.back_st();
            txtmanager.DisplayNextSentence();
            hasExecuted = true;
        }

        if (GAMEMANAGER.instance.game_start)
            frameCounter++;

        if (frameCounter >= framesPerAction && GAMEMANAGER.instance.game_start)
        { // n프레임 마다 한번
            // rand_n = Random.Range(2, 3);
            //animator.SetInteger("ani", 3);
            if (phase == 0)
                rand_n = 1;
            else if (phase == 1)
                rand_n = 2;
            else if (phase == 2)
                rand_n = 3;

            if (rand_n == 1) // 1페
            {
                StartCoroutine(FireCircleBullets());
            }
            else if (rand_n == 2) // 2페
            {
                StartCoroutine(FireBullets());
            }
            else if (rand_n == 3) // 3페
            {
                StartCoroutine(flowerBullets());
            }
            frameCounter = 0;
        }

        if (gameObject.transform.position.y > 4)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        StartCoroutine(Rest());
    }

    IEnumerator MovePosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            float[] positions = { -7.5f, -4.5f, -1.5f };
            float newX = positions[Random.Range(0, positions.Length)];
            // 새로운 위치로 부드럽게 이동
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = new Vector3(newX, transform.position.y, transform.position.z);
            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float startTime = Time.time;

            // 방향에 따른 애니메이션 설정
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
            if(GAMEMANAGER.instance.game_start){
                for (float i = 0f; i < 20f; i++)
                {
                    float angle = 0 + (18f * i);
                    Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                    Instantiate(bulletPrefab, bulletSpawnPoint.position, rotation);
                    Instantiate(bulletPrefab2, bulletSpawnPoint.position, rotation);
                }
                yield return new WaitForSeconds(0.3f);
                for (float i = 0f; i < 20f; i++)
                {
                    float angle = 9f + (18f * i);
                    Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                    Instantiate(bulletPrefab, bulletSpawnPoint.position, rotation);
                    Instantiate(bulletPrefab2, bulletSpawnPoint.position, rotation);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator FireBullets()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && phase == 1)
        {
            for (int j = 0; j < 5; j++)
            {
                if(GAMEMANAGER.instance.game_start){
                    Vector3 playerDirection = (player.transform.position - transform.position).normalized;
                    float baseAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
                    for (int i = 0; i < 5; i++)
                    {
                        float randomAngleOffset = Random.Range(-20f, 20f);
                        float angle = baseAngle + randomAngleOffset;
                        Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90);
                        Instantiate(bulletPrefab3, bulletSpawnPoint.position, rotation);
                        yield return new WaitForSeconds(1f);
                    }
                }
            }
        }
    }

    IEnumerator flowerBullets()
    {
        for (float i = 0f; i < 60f; i++)
        {
            if(GAMEMANAGER.instance.game_start){
                int b_ro = 60;
                float angle = 0 + (10f * i);
                //Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                //Quaternion rotation2 = Quaternion.Euler(0f, 0f, angle * -1f);
                for(int j = 0; j<6; j++){
                    Instantiate(bulletPrefab4, bulletSpawnPoint.position, Quaternion.Euler(0f, 0f, angle+(b_ro*j)));
                    Instantiate(bulletPrefab5, bulletSpawnPoint.position, Quaternion.Euler(0f, 0f, (angle * -1f) - (b_ro*j)));
                }
            }
            yield return new WaitForSeconds(0.1f); // 0.03초 간격으로 발사
        }
    }

    IEnumerator Rest()
    {
        yield return new WaitForSeconds(5.0f);
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

    IEnumerator RecoverHealth()
    {
        isRecovering = true;
        float recoverySpeed = 500f;

        while (HP < 1000)
        {
            HP += recoverySpeed * Time.deltaTime;
            if (HP > 1000)
            {
                HP = 1000;
            }
            yield return null;
        }

        isRecovering = false;
    }
}
