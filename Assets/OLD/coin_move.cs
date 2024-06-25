using UnityEngine;

public class coin_move : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float m_speed = 5.0f;
    [SerializeField] private float magnetDistance = 2.0f; // 플레이어와 코인 사이의 자석 거리
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        MoveTowardsPlayer();

        if (gameObject.transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= magnetDistance)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime * m_speed);
            }
            else{
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }
        
        if (gameObject.transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Soundmanager.Instance.Playsound("coin");
            GAMEMANAGER.instance.score += 300;
            Destroy(gameObject);
        }
    }
}
