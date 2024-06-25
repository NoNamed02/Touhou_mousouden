using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class credit : MonoBehaviour
{
    public RectTransform rect;
    Vector2 first_pos = new Vector2(0, -2776);
    public Image image;
    Color color = new Color(1, 1, 1, 0);

    void Start()
    {
        rect.anchoredPosition = first_pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (first_pos.y <= 1835)
        {
            first_pos.y += 0.5f;
            rect.anchoredPosition = first_pos;
        }
        if (color.a < 1)
        {
            color.a += 0.8f * Time.deltaTime;
            image.color = color;
        }
        if(Input.GetMouseButtonDown(0)){
            Soundmanager.Instance.Playsound("btn_choice");
            GAMEMANAGER.instance.LIFE = 5;
            GAMEMANAGER.instance.spellCard_count = 4;
            GAMEMANAGER.instance.enemy_break_count = 0;
            GAMEMANAGER.instance.score = 0;
            SceneManager.LoadScene(0);
        }
    }
}
