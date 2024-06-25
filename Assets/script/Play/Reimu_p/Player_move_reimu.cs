using UnityEngine;
using UnityEngine.UI; // 이미지 컴포넌트를 사용하려면 필요
using System.Collections;
using Unity.VisualScripting;

public class Player_move_reimu : MonoBehaviour
{
    public GameObject P_bullet;
    public GameObject P_bullet2;
    public GameObject P_bullet3;
    public GameObject P_bullet_m_n;
    public GameObject P_bullet_s_m;
    public Transform fire_position;
    public Transform fire_position1;
    public Transform fire_position2;
    [SerializeField] private int speed = 10;
    public int iter = 0;
    public int level = 1;
    private Animator animator;
    private Rigidbody2D rb;
    
    private BoxCollider2D boxCollider2D;
    private Vector2 minBounds = new Vector2(-9f, -5.5f);
    private Vector2 maxBounds = new Vector2(-0.8f, 5.5f);

    public GameObject spellcard;
    public GameObject spellcard2;
    public GameObject master_spark_1;
    public GameObject master_spark_2;
    public GameObject master_spark_3;
    
    public GameObject spellcard_bg;
    
    public GameObject spellcard_bg_m;
    public GameObject re_spellcard;
    private int spell_time = 0;
    private bool is_hit = false;
    private int respwan_time = 200;
    public SpriteRenderer image;

    
    public GameObject gameover;



    [SerializeField]private bool s_move_b = false;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        // 플레이어 초기 위치 설정
        transform.position = new Vector3(-4.5f, -2.5f, 0f);
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        image = GetComponent<SpriteRenderer>();
        boxCollider2D.enabled = true;
    }

    void Update()
    {
        if(GAMEMANAGER.instance.LIFE >= 0){
            if(GAMEMANAGER.instance.game_start == true){
                GAMEMANAGER.instance.score++;
                // 총알 발사
                if (Input.GetKey(KeyCode.Z)&& is_hit == false){
                    iter++;
                    if(GAMEMANAGER.instance.player_is_reimu){
                        if(iter % 8 == 0)
                        {
                            Instantiate(P_bullet, fire_position.position, Quaternion.identity);
                            if(level >= 20)
                            {
                                Instantiate(P_bullet2, fire_position2.position, Quaternion.identity);
                                Instantiate(P_bullet2, fire_position1.position, Quaternion.identity);
                            }
                            if(level >= 40){ // max 레벨
                                Instantiate(P_bullet3, fire_position2.position, Quaternion.identity);
                                Instantiate(P_bullet3, fire_position1.position, Quaternion.identity);
                            }
                        }
                    }
                    else{ // 마리사 총알
                        if(iter % 8 == 0)
                        {
                            Instantiate(P_bullet_m_n, fire_position.position, Quaternion.identity);
                            if(level >= 30){
                                if(s_move_b == true){
                                    Instantiate(P_bullet_s_m, fire_position.position, Quaternion.identity);
                                    //s_move_b = false;
                                }
                                else if(s_move_b == false){
                                    Quaternion rotation = Quaternion.Euler(0, 180, 0);
                                    Instantiate(P_bullet_s_m, fire_position.position, rotation);
                                    //s_move_b = true;
                                }
                                
                                if(s_move_b == true)
                                    s_move_b = false;
                                else if(s_move_b == false)
                                    s_move_b = true;
                            }
                        }
                        
                    }
                }
                else
                {
                    iter = 0;
                }
                if(Input.GetKeyDown(KeyCode.C) && spell_time == 0 && is_hit == false && GAMEMANAGER.instance.spellCard_count >= 1){
                    spell_time = 300;
                    GAMEMANAGER.instance.spellCard_count -=1;
                    if(GAMEMANAGER.instance.player_is_reimu){
                        Soundmanager.Instance.Playsound("freeze");
                        Instantiate(spellcard, transform.position, transform.rotation);
                        Instantiate(spellcard2, transform.position, transform.rotation);
                        Vector3 spell_bg = new Vector3(-4.1f, -0.619902f, 0f);
                        Instantiate(spellcard_bg, spell_bg, transform.rotation);
                    }
                    else{
                        Soundmanager.Instance.Playsound("razer");
                        Quaternion rot = transform.rotation;
                        rot = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, 90);

                        Instantiate(master_spark_1, transform.position, rot);
                        Instantiate(master_spark_2, transform.position, rot);
                        Instantiate(master_spark_3, transform.position, rot);

                        Vector3 spell_bg = new Vector3(-4.1f, -0.619902f, 0f);
                        Instantiate(spellcard_bg_m, spell_bg, transform.rotation);
                    }
                }
                if(spell_time != 0){
                    spell_time -=1;
                }
            }
        }
        
        // 플레이어 이동
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = (Vector3.up * v) + (Vector3.right * h);

        if (moveDir.magnitude > 1f)
        {
            moveDir = moveDir.normalized; // 입력 벡터를 정규화하여 길이가 1인 벡터로 만듦
        }

        if(!is_hit) // hit 상태가 아닐 때만 움직임
        {
            transform.Translate(moveDir * speed * Time.deltaTime);
        }
        if(GAMEMANAGER.instance.player_is_reimu){// 애니메이터 매개변수 설정
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
        }
        else{ // 마리사 애니
            if (h > 0)
            {
                animator.SetInteger("ani", 10); // 오른쪽 이동
            }
            else if (h < 0)
            {
                animator.SetInteger("ani", -10); // 왼쪽 이동
            }
            else
            {
                animator.SetInteger("ani", 100); // 이동 없음
            }
        }

        // 플레이어 위치 제한
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        transform.position = clampedPosition;

        if(is_hit == true){
            if(respwan_time == 200){
                level = 0;
                Soundmanager.Instance.Playsound("die");
                Instantiate(re_spellcard, transform.position, transform.rotation);
            }
            Color color = image.color;
            color.a = 0.0f; // 투명도 설정
            image.color = color;
            //boxCollider2D.enabled = false;
            transform.position = new Vector3(-4.5f, -2.5f, 0f); // 위치 고정
            respwan_time -= 1;
            if(respwan_time == 20)
                Soundmanager.Instance.Playsound("respwan");
            if(respwan_time <= 0){
                //boxCollider2D.enabled = true;
                respwan_time = 200;
                is_hit = false;
                color.a = 1f;
                image.color = color;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            level += 1;
            if (level >= 40)
            {
                level = 40;
            }
        }
        if (other.gameObject.CompareTag("e_bullet"))
        {
            if(is_hit == false && GAMEMANAGER.instance.LIFE >= 1){
                GAMEMANAGER.instance.LIFE -= 1;
                is_hit = true;
                level = 1;
            }
            else if(is_hit == false && GAMEMANAGER.instance.LIFE <= 0){
                Debug.Log("죽음!");
                gameover.SetActive(true);
                Color color = image.color;
                color.a = 0.0f; // 투명도 설정
                image.color = color;
                GAMEMANAGER.instance.LIFE -= 1;
            }
        }
    }
}
