using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_count : MonoBehaviour
{
    private int bomb_count; 
    public int des_point = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bomb_count = GAMEMANAGER.instance.spellCard_count;
        if(des_point > bomb_count){
            gameObject.SetActive(false);
        }
    }
}
