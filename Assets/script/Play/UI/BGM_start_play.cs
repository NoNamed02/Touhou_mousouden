using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_start_play : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GAMEMANAGER.instance.LIFE = 5;
        //GAMEMANAGER.instance.spellCard_count = 4;
        BGMmanager.Instance.Playsound("hakurei_play");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
