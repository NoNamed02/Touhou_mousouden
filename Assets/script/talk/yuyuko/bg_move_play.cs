using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_move_play : MonoBehaviour
{
    public string song_name = "";
    void Start()
    {
        transform.position = new Vector3(-5.27f, -4.42f, 0f);
        BGMmanager.Instance.Playsound(song_name);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sub = transform.position;
        if(sub.y < 4.12){
            sub.y += 0.002f;
            transform.position = sub;
        }
    }
}
