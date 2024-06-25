using UnityEngine;

public class Star : MonoBehaviour
{
    public float fadeDuration = 3f; // 페이드 아웃하는 데 걸리는 시간
    private float currentAlpha = 1f; // 현재 투명도
    private Vector3 direction; // 이동 방향

    void Start()
    {
        // 랜덤한 방향 설정
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }

    void Update()
    {
        // 회전
        transform.Rotate(Vector3.forward, 30f * Time.deltaTime);

        // 이동
        transform.position += direction * Time.deltaTime;

        // 투명도 감소
        currentAlpha -= Time.deltaTime / fadeDuration;
        if (currentAlpha <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // 색상 및 투명도 설정
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = currentAlpha;
        GetComponent<SpriteRenderer>().color = color;
    }
}
