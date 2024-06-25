using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boss : MonoBehaviour
{
    private Animator anim;
    private int speed = 5;
    private int frameCounter = 0;
    private int framesPerAction = 300; // n프레임마다 실행
    [SerializeField]private int HP = 3000;

    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    
    public GameObject sub;
    public Transform bulletSpawnPoint;
    public Transform Point1;
    public Transform Point2;
    private bool is_sub = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP < 0){
            GameManager.instance.scene_check = true;
            SceneManager.LoadScene("endScenes");
            Destroy(gameObject);
        }
        frameCounter++;
        if(frameCounter >= framesPerAction){//n프레임 마다 한번
            int rand_n = Random.Range(1, 4);

            if(rand_n == 1){
                // anim.set ---- 
                anim.SetInteger("is_fire",1);
                StartCoroutine(FireCircleBullets());
            }
            else if(rand_n == 2){
                anim.SetInteger("is_fire",1);
                StartCoroutine(FireBullets());
            }
            else if(rand_n == 3){
                anim.SetInteger("is_fire",1);
                StartCoroutine(CircleBullets());
            }
            frameCounter = 0;
        }
        if(gameObject.transform.position.y > -0.24){
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        else if(is_sub == false){
            Instantiate(sub, Point1.position, Point1.transform.rotation);
            Instantiate(sub, Point2.position, Point2.transform.rotation);
            is_sub = true;
        }
        
        StartCoroutine(rest());
    }
    IEnumerator FireCircleBullets()
{
    anim.SetInteger("is_fire", 1);
    for (int j = 0; j < 3; j++) // n번 반복
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
    anim.SetInteger("is_fire", 0); // 발사 종료 후 애니메이션 리셋
    
}

    IEnumerator FireBullets()
    {
        anim.SetInteger("is_fire",1);
        for (int i = 0; i < 20; i++) // 20번 반복
        {
            float angle = Random.Range(0, 360); // 무작위 각도 생성
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle); // 회전값 생성
            Vector3 direction = rotation * Vector3.up; // 총알의 이동 방향 계산

            Instantiate(bulletPrefab2, bulletSpawnPoint.position, rotation);
            yield return new WaitForSeconds(0.2f); // 0.4초 간격으로 발사
        }
        anim.SetInteger("is_fire", 0); // 발사 종료 후 애니메이션 리셋
    }
    IEnumerator CircleBullets()
    {
        anim.SetInteger("is_fire",1);
        //for (int j = 0; j < 2; j++) // n번 반복
        //{
            for(float i = 0f; i < 80f; i++){
                float angle = 0 + (18f*i);
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Instantiate(bulletPrefab3, bulletSpawnPoint.position, rotation);
                yield return new WaitForSeconds(0.1f); // 0.5초 간격으로 발사
            }
        //}
        
        anim.SetInteger("is_fire", 0); // 발사 종료 후 애니메이션 리셋
    }
    IEnumerator rest()
    {
        yield return new WaitForSeconds(3.0f); // n초 그로기
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject gameM = GameObject.FindGameObjectWithTag("GameM");
        if (collision.gameObject.CompareTag("player_bullet"))
        {
            HP -= 1;
            GameManager.instance.score += 20;
        }
    }
    public void DecreaseHP(int amount)
    {
        HP -= amount;
        StartCoroutine(FlashRed()); 
    }
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(2.0f); // 0.2초 동안 빨갛게 표시
        spriteRenderer.color = Color.white;
    }
}
