using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMmanager : MonoBehaviour
{
    //private static BGMmanager instance;
    public static BGMmanager Instance;
    public AudioSource Sound;
    public AudioClip Main;
    public AudioClip hakurei;
    public AudioClip hakurei_play;
    public AudioClip remilia;
    public AudioClip remilia_play;
    public AudioClip yuyuko_t;
    public AudioClip yuyuko_play;
    public AudioClip yuyuko_boss_play;
    public AudioClip yukari_boss_play;
    public AudioClip end_bad_apple;
    public string sound_name;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioSource가 null일 경우 추가
            if (Sound == null)
            {
                Sound = gameObject.AddComponent<AudioSource>();
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void Playsound(string n){
        sound_name = n;
        switch(sound_name){
            case "Main":{
                Sound.clip = Main;
                break;
            }
            case "hakurei":{
                Sound.clip = hakurei;
                break;
            }
            case "hakurei_play":{
                Sound.clip = hakurei_play;
                break;
            }
            case "remilia":{
                Sound.clip = remilia;
                break;
            }
            case "remilia_play":{
                Sound.clip = remilia_play;
                break;
            }
            case "yuyuko_t":{
                Sound.clip = yuyuko_t;
                break;
            }
            case "yuyuko_play":{
                Sound.clip = yuyuko_play;
                break;
            }
            case "yuyuko_boss_play":{
                Sound.clip = yuyuko_boss_play;
                break;
            }
            case "yukari_boss_play":{
                Sound.clip = yukari_boss_play;
                break;
            }
            case "end_bad_apple":{
                Sound.clip = end_bad_apple;
                break;
            }
        }
        Sound.Play();
    }
}
