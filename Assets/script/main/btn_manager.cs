using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btn_manager : MonoBehaviour
{
    public Button start;
    public Button option;
    public Button quit;
    public GameObject credit;

    public GameObject Loading;
    void Start()
    {
        BGMmanager.Instance.Playsound("Main");
        start.onClick.AddListener(start_);
        option.onClick.AddListener(option_);
        quit.onClick.AddListener(quit_);
    }

    void start_(){
        Soundmanager.Instance.Playsound("btn_choice");
        Loading.SetActive(true);
    }
    void option_(){
        Soundmanager.Instance.Playsound("btn_choice");
        credit.SetActive(true);
    }
    void quit_(){
        Soundmanager.Instance.Playsound("btn_choice");
        Application.Quit();
    }
}
