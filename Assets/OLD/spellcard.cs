using UnityEngine;

public class spellcard : MonoBehaviour
{
    public float maxScale = 5f; // 오브젝트가 도달할 최대 크기
    public float scaleSpeed = 0.5f; // 크기가 증가하는 속도

    private void Start()
    {
        AudioSource spell = GetComponent<AudioSource>();
        spell.Play();
        // 시작할 때 크기를 가장 작게 설정
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        // 현재 크기가 최대 크기보다 작으면 크기를 증가시킴
        if (transform.localScale.x < maxScale)
        {
            Vector3 newScale = transform.localScale + Vector3.one * scaleSpeed * Time.deltaTime;
            // 새로운 크기가 최대 크기를 초과하지 않도록 제한
            newScale = Vector3.Min(newScale, Vector3.one * maxScale);
            transform.localScale = newScale;
        }
        else
        {
            // 최대 크기에 도달하면 오브젝트를 파괴
            Destroy(gameObject);
        }
    }
}
