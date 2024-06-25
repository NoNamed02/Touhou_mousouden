using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_play : MonoBehaviour
{
    public string song_name = "";
    void Start()
    {
        BGMmanager.Instance.Playsound(song_name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
