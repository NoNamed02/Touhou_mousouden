using System.Collections;
using UnityEngine;

public class talk_bg : MonoBehaviour
{
    [SerializeField] private float duration = 2.0f;
    public GameObject name_Text;
    public GameObject talk_Text;
    public GameObject txtmanager;
    void Start()
    {
        back_st();
        name_Text.SetActive(false);
        talk_Text.SetActive(false);
        txtmanager.SetActive(false);
    }
    public void back_st(){
        transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        StartCoroutine(ChangeScaleUp());
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ChangeScaleDown());
        }
        */
    }

    public IEnumerator ChangeScaleUp()
    {
        Vector3 startScale = new Vector3(0.1f, 0.1f, 1f);
        Vector3 midScale = new Vector3(8f, 0.1f, 1f);
        Vector3 endScale = new Vector3(8f, 2f, 1f);

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, midScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = midScale; // 마지막 스케일 설정

        // 두 번째 단계: 0.1,2,1 -> 8,2,1
        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(midScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale; // 마지막 스케일 설정
        name_Text.SetActive(true);
        talk_Text.SetActive(true);
        txtmanager.SetActive(true);
    }

    public IEnumerator ChangeScaleDown()
    {
        // 최종 스케일
        Vector3 startScale = new Vector3(8f, 2f, 1f);
        // 중간 스케일
        Vector3 midScale = new Vector3(0.1f, 2f, 1f);
        // 초기 스케일
        Vector3 endScale = new Vector3(0.1f, 0.1f, 1f);

        // 첫 번째 단계: 8,2,1 -> 0.1,2,1
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, midScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = midScale; // 마지막 스케일 설정

        // 두 번째 단계: 0.1,2,1 -> 0.1,0.1,1
        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(midScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale; // 마지막 스케일 설정
        gameObject.SetActive(false);
    }
}
