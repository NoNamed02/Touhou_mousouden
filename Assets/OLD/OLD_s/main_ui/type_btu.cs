using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class type_btu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale;
    public ButtonType currentType;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case ButtonType.Type:
                Debug.Log("type_c");
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트를 찾음
                if (playerObj != null)
                {
                    Player_move player = playerObj.GetComponent<Player_move>(); // 플레이어 컨트롤러 스크립트를 가져옴
                    if (player != null)
                    {
                        if (player.type == 0)
                        {
                            player.type = 1;
                        }
                        else
                        {
                            player.type = 0;
                        }
                    }
                }
                break;
                
        }
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

    /*
    // 버튼을 눌렀을 때 호출되는 함수
    public void ChangePlayerType()
    {
        Debug.Log("AAAAAAAAAAA");
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트를 찾음
        if (playerObj != null)
        {
            Player_move player = playerObj.GetComponent<Player_move>(); // 플레이어 컨트롤러 스크립트를 가져옴
            if (player != null)
            {
                if (player.type == 0)
                {
                    player.type = 1;
                }
                else
                {
                    player.type = 0;
                }
            }
        }
    }
    */

