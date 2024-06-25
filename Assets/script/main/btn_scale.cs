using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btn_scale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale;
    private Vector3 defaultScale;
    public Image image;

    void Start()
    {
        defaultScale = buttonScale.localScale;
        image = GetComponent<Image>();
    }

    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Soundmanager.Instance.Playsound("main_btn");
        buttonScale.localScale = defaultScale * 1.2f;
        StartCoroutine(FadeOut());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        while (image.color.a < 1f)
        {
            Color color = image.color;
            color.a += 0.05f;
            image.color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeOut()
    {
        while (image.color.a > 0.5f)
        {
            Color color = image.color;
            color.a -= 0.05f;
            image.color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
