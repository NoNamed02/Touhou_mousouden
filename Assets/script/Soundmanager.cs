using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    private static Soundmanager instance;
    public static Soundmanager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<Soundmanager>();
                singletonObject.name = typeof(Soundmanager).ToString() + " (Singleton)";
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    
    public AudioSource Sound;
    public AudioClip main_btn;
    public AudioClip btn_choice;
    public AudioClip glass_break;
    public AudioClip razer;
    public AudioClip freeze;
    public AudioClip coin;
    public AudioClip power;
    public AudioClip die;
    public AudioClip respwan;



    public string sound_name;

    public void Playsound(string n){
        sound_name = n;
        switch(sound_name){
            case "main_btn":
                Sound.clip = main_btn;
                break;
            case "btn_choice":
                Sound.clip = btn_choice;
                break;
            case "glass_break":
                Sound.clip = glass_break;
                break;
            case "razer":
                Sound.clip = razer;
                break;
            case "freeze":
                Sound.clip = freeze;
                break;
            case "coin":
                Sound.clip = coin;
                break;
            case "power":
                Sound.clip = power;
                break;
            case "die":
                Sound.clip = die;
                break;
            case "respwan":
                Sound.clip = respwan;
                break;
        }
        Sound.Play();
    }
}
