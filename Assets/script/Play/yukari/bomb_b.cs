using System.Collections;
using UnityEngine;

public class bomb_b : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;
    private bool isExploding = false;
    public GameObject bulletPrefab;

    void Start()
    {
        direction = transform.up;
        speed = Random.Range(2f,5f);
    }

    void Update()
    {
        if (GAMEMANAGER.instance.game_start == false)
            Destroy(gameObject);

        if (!isExploding)
        {
            transform.position += direction * speed * Time.deltaTime;
            speed = Mathf.Max(0, speed - Time.deltaTime); // 속도 감속

            if (speed == 0)
            {
                StartCoroutine(Explode());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("spellcard"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Explode()
    {
        isExploding = true;
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 16; i++)
        {
            float angle = i * 22.5f;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab, transform.position, rotation);
        }

        Destroy(gameObject);
    }
}
