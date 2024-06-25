using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class choice_btn_manager : MonoBehaviour
{
    public Button c_r;
    public Button c_m;

    public GameObject Loading;
    public GameObject Loading2;
    void Start()
    {
        //BGMmanager.Instance.Playsound("Main");
        c_r.onClick.AddListener(cr);
        c_m.onClick.AddListener(cm);
    }

    void cr(){
        Soundmanager.Instance.Playsound("btn_choice");
        Loading.SetActive(true);
    }
    void cm(){
        Soundmanager.Instance.Playsound("btn_choice");
        Loading2.SetActive(true);
    }
}
