using UnityEngine;
using System.Collections;

public class Player_move_2rd : MonoBehaviour
{
    public GameObject P_bullet;
    public Transform fire_position;
    public Transform fire_position1;
    public Transform fire_position2;
    [SerializeField] private int speed = 10;
    private int iter = 0;
    public int level = 1;

    private bool spwan_check;
    private Animator animator;

    private Vector2 minBounds = new Vector2(-8.78f, -5.35f);
    private Vector2 maxBounds = new Vector2(-0.6f, 5.22f);

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        // 플레이어 초기 위치 설정
        transform.position = new Vector3(-4.67f, -3.31f, 0f);

        animator = GetComponent<Animator>();
        spwan_check = true;
    }

    void Update()
    {
        GAMEMANAGER.instance.score++;
        AudioSource die = GetComponent<AudioSource>();
        // 플레이어 이동
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = (Vector3.up * v) + (Vector3.right * h);

        if (moveDir.magnitude > 1f)
        {
            moveDir = moveDir.normalized; // 입력 벡터를 정규화하여 길이가 1인 벡터로 만듦
        }

        transform.Translate(moveDir * speed * Time.deltaTime);

        // 애니메이터 매개변수 설정
        if (h > 0)
        {
            animator.SetInteger("ani", 1); // 오른쪽 이동
        }
        else if (h < 0)
        {
            animator.SetInteger("ani", -1); // 왼쪽 이동
        }
        else
        {
            animator.SetInteger("ani", 0); // 이동 없음
        }

        // 플레이어 위치 제한
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        transform.position = clampedPosition;

        // 총알 발사
        if (Input.GetKey(KeyCode.Z))
        {
            iter++;
            if (iter % 8 == 0)
            {
                for (int i = 0; i < level; i++)
                {
                    Instantiate(P_bullet, fire_position.position, Quaternion.identity);
                    if (level >= 10)
                    {
                        Instantiate(P_bullet, fire_position1.position, Quaternion.identity);
                    }
                    if (level >= 20)
                    {
                        Instantiate(P_bullet, fire_position2.position, Quaternion.identity);
                    }
                }
            }
        }
        else
        {
            iter = 0; // Reset the iter when the key is not pressed to ensure continuous firing intervals
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("item"))
        {
            level += 1;
            if (level >= 30)
            {
                level = 30;
            }
        }
    }
}
