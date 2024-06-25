using UnityEngine;

public class item_move : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints; // 베지어 곡선 제어점
    public float movementSpeed = 2f; // 이동 속도
    private float t = 0f;
    private bool isForward = true;

    private void Start()
    {
        waypoints = new Transform[4];

        for (int i = 0; i < waypoints.Length; i++)
        {
            GameObject waypointObj = new GameObject("Waypoint_" + i);
            waypoints[i] = waypointObj.transform;
        }

        waypoints[0].position = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y, 0f);
        waypoints[1].position = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 0.5f, 0f);
        waypoints[2].position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.5f, 0f);
        waypoints[3].position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, 0f);
    }
    /* - waypoints 초기화 안됨 위는 수정코드
    waypoints = new Transform[4];
    for (int i = 0; i < waypoints.Length; i++) // bazier을 위한 4개 점 오브젝트 // 랜덤한 좌표 생성하여 waypoints 배열에 할당 - 취소
    {
        GameObject waypointObj = new GameObject("Waypoint_" + i);
        waypoints[i] = waypointObj.transform;
    }
    waypoints = new Transform[4];

    GameObject waypointObj = new GameObject("Waypoint_" + 0);
    waypoints[0].position = new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 3, 0f);
    waypoints[0] = waypointObj.transform;

    GameObject waypointObj1 = new GameObject("Waypoint_" + 1);
    waypoints[1].position = new Vector3(gameObject.transform.position.x - 3, gameObject.transform.position.y + 3, 0f);
    waypoints[1] = waypointObj1.transform;

    GameObject waypointObj2 = new GameObject("Waypoint_" + 2);
    waypoints[2].position = new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y - 3, 0f);
    waypoints[2] = waypointObj2.transform;

    GameObject waypointObj3 = new GameObject("Waypoint_" + 1);
    waypoints[3].position = new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y - 3, 0f);
    waypoints[3] = waypointObj3.transform;
    
}
*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        MoveBackAndForth();
    }

    private void MoveBackAndForth()
    {
        if (isForward)
        {
            t += Time.deltaTime * movementSpeed; // 순방향으로 이동

            if (t >= 1f)
            {
                t = 1f; // t 값이 1을 초과하면 1로 설정하고 역방향으로 전환
                isForward = false;
            }
        }
        else
        {
            t -= Time.deltaTime * movementSpeed; // 역방향으로 이동

            if (t <= 0f)
            {
                t = 0f; // t 값이 0보다 작으면 0으로 설정하고 순방향으로 전환
                isForward = true;
            }
        }

        Vector3 newPosition = CalculateBezierPosition(t);
        transform.position = newPosition;
    }

    private Vector3 CalculateBezierPosition(float t) // bazier 계산 함수
    {
        // 베지어 곡선 계산식을 사용하여 현재 t 값에 해당하는 위치를 계산
        Vector3 position = Mathf.Pow(1 - t, 3) * waypoints[0].position +
                           3 * Mathf.Pow(1 - t, 2) * t * waypoints[1].position +
                           3 * (1 - t) * Mathf.Pow(t, 2) * waypoints[2].position +
                           Mathf.Pow(t, 3) * waypoints[3].position;

        return position;
    }
}
