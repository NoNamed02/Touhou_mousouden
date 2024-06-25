using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class spellcard_btu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale;
    public ButtonType currentType;
    public GameObject spell;
    public GameObject star;
    public GameObject image1;
    public RectTransform canvasRectTransform; // Canvas의 RectTransform 참조 추가
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    public void OnBtnClick_2()
    {
        switch (currentType)
        {
            case ButtonType.Spell:
                Debug.Log("boom_spellcard");
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트를 찾음
                if (playerObj != null && GameManager.instance.spellCard_count > 0)
                {
                    GameManager.instance.spellCard_count -= 1;
                    Instantiate(spell, new Vector3(0, -2f, 0), gameObject.transform.rotation);
                    for (int i = 0; i < 4; i++)
                        Instantiate(star, new Vector3(0, -2f, 0), gameObject.transform.rotation);
                    Instantiate(spell, new Vector3(0, -2f, 0), gameObject.transform.rotation);
                    Instantiate(image1, new Vector3(0, -2f, 0), gameObject.transform.rotation);
                }
                break;
        }
    }

    void InstantiateUI(GameObject prefab, Vector3 desiredPosition)
    {
        // Canvas의 상대적인 위치 계산
        Vector2 anchoredPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, desiredPosition);
        RectTransform rectTransform = Instantiate(prefab, canvasRectTransform).GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.localRotation = Quaternion.identity;
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        buttonScale.localScale = defaultScale;
    }
}
