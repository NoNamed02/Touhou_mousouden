using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Btn_type : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale;
    public ButtonType currentType;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
        
        AudioSource BUTTON = GetComponent<AudioSource>();
    }
    public void OnBtnClick()
    {
        AudioSource BUTTON = GetComponent<AudioSource>();
        switch (currentType)
        {
            case ButtonType.New:
                BUTTON.Play();
                SceneManager.LoadScene("Loading");
                Debug.Log("새게임");
                break;

            case ButtonType.Continue:
                Debug.Log("이어하기");
                break;
            case ButtonType.Quit:
                BUTTON.Play();
                Debug.Log("나가기");
                Application.Quit();
                break;
                
        }
    }
    public void OnPointerEnter(PointerEventData eventdata)
    {
        AudioSource BUTTON = GetComponent<AudioSource>();
        BUTTON.Play();
        buttonScale.localScale = defaultScale * 1.2f;
    }
    public void OnPointerExit(PointerEventData eventdata)
    {
        buttonScale.localScale = defaultScale;
    }
}
