using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fairy_guided : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform player; // 플레이어의 Transform
    private float iter = 0;
    public GameObject item;
    public GameObject coin;
    private Animator anim;
    private bool is_hit = false;
    Vector3 unit;

    public float duration = 1f; // 이동에 걸리는 시간
    [SerializeField]private Vector3 startPoint, endPoint, controlPoint; 
    [SerializeField]private float timer = 0f;
    
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("is_move", 0);
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else if (player == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다!");
        }
        // 플레이어를 태그로 찾아 할당
        /*
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다!");
        }
        */
        /****************************bazier곡선********************************/
        // 시작점은 현재 위치로 설정
        startPoint = transform.position;

        // 끝점은 현재 위치의 x 좌표에 6을 더한 값으로 설정
        endPoint = new Vector3(transform.position.x + 6f, transform.position.y, transform.position.z);

        Vector3 controlPointPosition = new Vector3((transform.position.x + endPoint.x) / 2f, transform.position.y - 5f, transform.position.z);
        controlPoint = controlPointPosition;
        /************************************************************/
    }

    void Update()
    {
        iter++;

    /**************************************************************************************/
        timer += Time.deltaTime;
        if (timer > duration)
        {
            timer = duration;
        }
        if(timer == 1 && iter % 15 == 0)
        {
            if(is_hit == false)
                Shoot();
            animator.SetInteger("is_move", 1);
        }

        // 베지어 곡선 계산
        float t = timer / duration;
        Vector3 newPosition = CalculateBezierPoint(t, startPoint, controlPoint, endPoint);

        // 오브젝트 이동
        transform.position = newPosition;
    /**************************************************************************************/
        if(GAMEMANAGER.instance.game_start == false)
            Destroy(gameObject);
    }

    void Shoot()
    {
        if (player == null)
            return;

        // 총알이 플레이어를 향해 발사되도록 방향 계산
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // 총알 생성
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, rotation);
        // 총알 방향 설정
        newBullet.transform.up = direction;
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0; // (1-t)^2 * P0
        p += 2 * u * t * p1; // 2 * (1-t) * t * P1
        p += tt * p2; // t^2 * P2
        return p;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject gameM = GameObject.FindGameObjectWithTag("GameM");
        if (collision.gameObject.CompareTag("player_bullet") && is_hit == false)
        {
            animator.SetInteger("is_hit_ani",1);
            //manager.score += 100;
            //GameManager.instance.score += 100;
            //GameManager.instance.boss_spwan += 1;
            Debug.Log("hit!!!!!!!GGG");
            //anim.Play("fairy_die");
            StartCoroutine(die());
            is_hit = true;
        }
        
        if (collision.gameObject.CompareTag("spellcard"))
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator die()
    {
        AudioSource sound = GetComponent<AudioSource>();
        sound.Play();
        int rand_n = Random.Range(1, 4);
        if(rand_n == 3){
            Instantiate(coin, transform.position, transform.rotation);
        }
        Instantiate(item, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
