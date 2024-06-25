using System.Collections;
using UnityEngine;

public class MaterialFog : MonoBehaviour
{
    [SerializeField] private Material fogMaterial;
    [SerializeField] private float minAlpha = 0.1f;
    [SerializeField] private float maxAlpha = 0.4f;
    [SerializeField] private float speed = 1.0f;

    private bool increasing = true;

    void Start()
    {
        StartCoroutine(ChangeAlpha());
    }

    IEnumerator ChangeAlpha()
    {
        Color color = fogMaterial.color;

        while (true)
        {
            if (increasing)
            {
                color.a += speed * Time.deltaTime;
                if (color.a >= maxAlpha)
                {
                    color.a = maxAlpha;
                    increasing = false;
                }
            }
            else
            {
                color.a -= speed * Time.deltaTime;
                if (color.a <= minAlpha)
                {
                    color.a = minAlpha;
                    increasing = true;
                }
            }

            fogMaterial.color = color;

            yield return null;
        }
    }
}
