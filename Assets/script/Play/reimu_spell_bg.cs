using System.Collections;
using UnityEngine;

public class reimu_spell_bg : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        //GAMEMANAGER.instance.LIFE = 5;
        //GAMEMANAGER.instance.spellCard_count = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        float alpha = 0f;
        while (alpha < 0.8f)
        {
            alpha += Time.deltaTime / 1.0f; 
            SetAlpha(alpha);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / 1.0f;
            SetAlpha(alpha);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
