using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class master_spark : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    private SpriteRenderer spriteRenderer;
    private Vector3 initialScale = new Vector3(6f, 0.1f, 1f);
    private Vector3 targetScale = new Vector3(6f, 4f, 1f);
    
    public float target_s = 4f;
    private float expandDuration = 1f;
    private float fadeDuration = 2f;
    private float rainbowDuration = 0.1f; // 색이 변하는 속도

    void Start()
    {
        //Soundmanager.Instance.Playsound("razer");
        transform.localScale = initialScale;
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else if (player == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다!");
        }

        StartCoroutine(ScaleAndFade());
        StartCoroutine(RainbowEffect()); // 무지개 효과 코루틴 시작
    }

    void Update()
    {
        if (player != null)
        {
            gameObject.transform.position = player.transform.position;
        }
    }

    private IEnumerator ScaleAndFade()
    {
        targetScale = new Vector3(6f, target_s, 1f);
        // 스케일을 증가
        float timer = 0f;
        while (timer < expandDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, timer / expandDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        // 투명도를 감소시키며 스케일을 일정하게 증가
        timer = 0f;
        Color initialColor = spriteRenderer.color;
        Vector3 additionalScale = new Vector3(0f, 1f, 0f);
        Vector3 finalScale = targetScale + additionalScale;
        
        while (spriteRenderer.color.a > 0)
        {
            Color newColor = spriteRenderer.color;
            newColor.a -= Time.deltaTime / fadeDuration; // 알파 값을 감소
            spriteRenderer.color = newColor;

            transform.localScale = Vector3.Lerp(targetScale, finalScale, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // 오브젝트 파괴
        Destroy(gameObject);
    }

    private IEnumerator RainbowEffect()
    {
        // 무작위 색상으로 시작
        float randomStart = Random.Range(0f, 1f);
        while (true)
        {
            Color currentColor = Color.HSVToRGB(Mathf.PingPong((Time.time + randomStart) * rainbowDuration, 1), 1, 1);
            currentColor.a = spriteRenderer.color.a; // 현재 투명도를 유지
            spriteRenderer.color = currentColor;
            yield return null;
        }
    }
}
