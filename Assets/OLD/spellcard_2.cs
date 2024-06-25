using System.Collections;
using UnityEngine;

public class spellcard_2 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private Vector3 startPosition;
    private float fadeDuration = 1f;
    private float holdDuration = 1f;
    //private bool isFading = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        startPosition = new Vector3(0.3f, -1.88f, -10f);

        // 시작 위치 설정
        transform.position = startPosition;

        // 시작 투명도 설정 (0이 아닌 값으로)
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0.01f);

        // 투명도 변경 시작
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        while (true)
        {
            // 투명도 0.5까지 증가
            yield return FadeTo(0.5f, fadeDuration);

            // 1초 동안 유지
            yield return new WaitForSeconds(holdDuration);

            // 투명도 0으로 감소
            yield return FadeTo(0f, fadeDuration);
        }
    }

    IEnumerator FadeTo(float targetAlpha, float duration)
    {
        //isFading = true;

        float currentTime = 0f;
        float startAlpha = spriteRenderer.color.a;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / duration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        //isFading = false;
    }

    void Update()
    {
        // 투명도가 0이 되면 파괴
        if (spriteRenderer.color.a == 0f)
        {
            Destroy(gameObject);
        }
    }
}
