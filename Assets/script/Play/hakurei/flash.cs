using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class flash : MonoBehaviour
{
    public Image image;

    void Start()
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color color = image.color;
            color.a = t;
            image.color = color;
            yield return null;
        }

        Color finalColor = image.color;
        finalColor.a = 1f;
        image.color = finalColor;

        yield return new WaitForSeconds(1f);

        for (float t = 1.0f; t > 0.0f; t -= Time.deltaTime)
        {
            Color color = image.color;
            color.a = t;
            image.color = color;
            yield return null;
        }

        finalColor.a = 0f;
        image.color = finalColor;
    }

    void Update()
    {
    }
}
