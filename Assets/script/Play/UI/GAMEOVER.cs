using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GAMEOVER : MonoBehaviour
{
    public Button retry;
    
    public Button main_menu;
    private RectTransform rectTransform;

    void Start()
    {
        retry.onClick.AddListener(re);
        main_menu.onClick.AddListener(mainmenu);
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-511f, 149f);
    }

    void mainmenu()
    {
        Soundmanager.Instance.Playsound("btn_choice");
        GAMEMANAGER.instance.LIFE = 5;
        GAMEMANAGER.instance.spellCard_count = 4;
        GAMEMANAGER.instance.enemy_break_count = 0;
        SceneManager.LoadScene(0);
    }
    void re()
    {
        Soundmanager.Instance.Playsound("btn_choice");
        GAMEMANAGER.instance.game_start = false;
        Scene currentScene = SceneManager.GetActiveScene();
        GAMEMANAGER.instance.LIFE = 5;
        GAMEMANAGER.instance.spellCard_count = 4;
        GAMEMANAGER.instance.enemy_break_count = 0;
        SceneManager.LoadScene(currentScene.name);
    }
}
