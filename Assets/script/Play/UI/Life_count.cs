using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_count : MonoBehaviour
{
    private int life_count; 
    public int des_point = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life_count = GAMEMANAGER.instance.LIFE;
        if(des_point > life_count){
            gameObject.SetActive(false);
        }
    }
}
