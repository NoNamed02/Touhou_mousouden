using UnityEngine;

public class Item : MonoBehaviour
{
    //private Vector3 direction; // 총알의 방향
    [SerializeField] private float speed = 5.0f;

    private void Start()
    {
        //direction = transform.up;
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Soundmanager.Instance.Playsound("power");
            GAMEMANAGER.instance.score += 300;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        //transform.position += direction * speed * Time.deltaTime;
        if (gameObject.transform.position.y <= -6)
        { // 거리 벌어지면 파괴
            Destroy(gameObject);
        }
    }

}
