using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 2f; // 적의 이동 속도
    public int i = 0;
    public int die_bool = 0;
    public GameObject E_bullet;
    public GameObject P_item;
    public GameObject spawn;
    public Transform e_f_pos;
    private Animator _animator;
    private float t = 0f; // 베지어 곡선 이동 변수
    private float diecheck = 0.0f;

    private Vector3[] waypoints; // 적이 따라갈 베지어 곡선의 제어점들
    public Transform playerTransform;
    private Collider2D hitCollider;
    private int move_check = 0;

    private void Start()
    {
        hitCollider = GetComponent<Collider2D>();
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;
        _animator = GetComponent<Animator>();

        // 랜덤한 좌표를 생성하여 waypoints 배열에 할당
        waypoints = new Vector3[4];
        waypoints[0] = new Vector3(spawn.transform.position.x, spawn.transform.position.y, 0f);
        waypoints[1] = new Vector3(spawn.transform.position.x, spawn.transform.position.y + 4, 0f);
        waypoints[2] = new Vector3(spawn.transform.position.x + 8 + Random.Range(-1.5f, 1.5f), spawn.transform.position.y + 4 + Random.Range(-1.5f, 1.5f), 0f);
        waypoints[3] = new Vector3(spawn.transform.position.x + 8 + Random.Range(-1.5f, 1.5f), spawn.transform.position.y + Random.Range(-1.5f, 1.5f), 0f);
    }

    private void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "bullet")
        {
            Debug.Log("hit check***************");
            if (die_bool == 0)
            {
                var itemDrop = Instantiate<GameObject>(this.P_item);
                itemDrop.transform.position = this.gameObject.transform.position;
                itemDrop.SetActive(true);
                hitCollider.enabled = false; // die 애니메이션 진입시 충돌 끔
                die_bool += 1;
            }
            diecheck = 1.0f;
        }
    }

    /* 수정 전 코드
    if (Coll.tag == "bullet")
    {
        Debug.Log("hit check***************");
        if (die_bool == 0)
        {
            Instantiate(P_item, gameObject.transform.position, Quaternion.identity);
            P_item.transform.position = gameObject.transform.position;
            die_bool += 1;
        }
        diecheck = 1.0f;
    }
    */

    private void Update()
    {
        _animator.SetFloat("diecheck", diecheck);
        if (move_check == 0) MoveAlongBezierCurve();
        if (t == 1f)
        {
            i++;
            //LookAtPlayer();
            if (move_check == 0)
            {
                LookAtPlayer();
                if (i % 100 == 0)
                {
                    GameObject bullet = Instantiate(E_bullet, e_f_pos.position + new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                    bullet.transform.rotation = transform.rotation;
                }
                if (i > 500) move_check = 1;
            }
            else if (move_check == 1)
            {
                hitCollider.enabled = false;
                float A = gameObject.transform.rotation.z;
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, A));
                gameObject.transform.Translate(gameObject.transform.forward * 1.0f * Time.deltaTime);
            }
        }
        if (gameObject.transform.position.y < -10) Destroy(gameObject);
    }

    private void MoveAlongBezierCurve()
    {
        t += Time.deltaTime * movementSpeed; // t 값을 이동 속도에 따라 증가
        if (t > 1f)
        {
            t = 1f;
        }

        Vector3 newPosition = CalculateBezierPosition(t);
        transform.position = newPosition;
        // 이동 방향을 바라보도록 회전
        Vector3 direction = CalculateBezierDirection(t);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 적 회전 설정
        transform.eulerAngles = new Vector3(0f, 0f, angle + 90f);
    }

    private Vector3 CalculateBezierPosition(float t)
    {
        Vector3 position = Mathf.Pow(1 - t, 3) * waypoints[0] +
                           3 * Mathf.Pow(1 - t, 2) * t * waypoints[1] +
                           3 * (1 - t) * Mathf.Pow(t, 2) * waypoints[2] +
                           Mathf.Pow(t, 3) * waypoints[3];
        return position;
    }

    private Vector3 CalculateBezierDirection(float t)
    {
        Vector3 tangent = (3 * Mathf.Pow(1 - t, 2) * (waypoints[1] - waypoints[0])) +
                          (6 * (1 - t) * t * (waypoints[2] - waypoints[1])) +
                          (3 * Mathf.Pow(t, 2) * (waypoints[3] - waypoints[2]));
        return tangent.normalized;
    }
    private void LookAtPlayer() // 플레이어를 보도록 함
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
    }
}
