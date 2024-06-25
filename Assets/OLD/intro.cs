using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class intro : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.0f; // 이미지가 사라지는 데 걸리는 시간
    [SerializeField] private float waitDuration = 1.0f; // 완전히 투명한 상태로 유지할 시간

    private void Start()
    {
        // Image 컴포넌트 가져오기
        Image image = GetComponent<Image>();

        // 이미지의 투명도를 조절하여 시작 페이드인 효과를 줍니다.
        StartCoroutine(FadeImage(image, 0.1f, 1.0f, fadeDuration));
    }

    // 이미지를 페이드 인/아웃하는 코루틴 함수
    private IEnumerator FadeImage(Image image, float startAlpha, float targetAlpha, float duration)
    {
        Color color = image.color;
        float currentTime = 0.0f;

        // 시작 투명도 설정
        color.a = startAlpha;
        image.color = color;

        // 페이드 인
        while (currentTime < duration)
        {
            // 현재 시간의 비율 계산
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / duration);

            // 투명도를 설정하고 대기
            color.a = alpha;
            image.color = color;
            yield return null;
        }

        // 투명도가 최대가 되었으면 잠시 기다린 후 투명도를 낮춥니다.
        yield return new WaitForSeconds(waitDuration);

        currentTime = 0.0f;

        // 페이드 아웃
        while (currentTime < duration)
        {
            // 현재 시간의 비율 계산
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(targetAlpha, 0.0f, currentTime / duration);

            // 투명도를 설정하고 대기
            color.a = alpha;
            image.color = color;
            yield return null;
        }

        // 페이드 아웃이 완료되면 이미지 오브젝트를 파괴합니다.
        Destroy(gameObject);
    }
}
