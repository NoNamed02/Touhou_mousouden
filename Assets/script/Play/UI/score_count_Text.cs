using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_count_Text : MonoBehaviour
{
    Text text;
    void Start()
    {
        GAMEMANAGER.instance.LIFE = 5;
        GAMEMANAGER.instance.spellCard_count = 4;
        GAMEMANAGER.instance.enemy_break_count = 0;
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = GAMEMANAGER.instance.score.ToString();
    }
}
