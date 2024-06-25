using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spellcard_image : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private float waitDuration = 1.0f;

    private void Start()
    {
        Image image = GetComponent<Image>();

        StartCoroutine(FadeImage(image, 0.1f, 0.5f, fadeDuration)); // 투명도의 최대값을 0.5로 수정
    }

    private IEnumerator FadeImage(Image image, float startAlpha, float targetAlpha, float duration)
    {
        Color color = image.color;
        float currentTime = 0.0f;

        color.a = startAlpha;
        image.color = color;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / duration);

            color.a = alpha;
            image.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(waitDuration);

        currentTime = 0.0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(targetAlpha, 0.0f, currentTime / duration);

            color.a = alpha;
            image.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}
