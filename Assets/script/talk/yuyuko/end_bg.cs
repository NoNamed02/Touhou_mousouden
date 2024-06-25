using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_bg : MonoBehaviour
{
    public GameObject reimu;
    public GameObject marisa;
    public GameObject yuyuko;
    public string song_name = "";
    void Start()
    {
        //BGMmanager.Instance.Sound.Stop();
        //BGMmanager.Instance.Playsound(song_name);
    }

    // Update is called once per frame
    void Update()
    {
        reimu.SetActive(false);
        marisa.SetActive(false);
        yuyuko.SetActive(false);
    }
}
