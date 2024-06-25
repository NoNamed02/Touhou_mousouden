using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_choice : MonoBehaviour
{
    public Button c_m;
    public Button c_r;
    public GameObject loading;
    void Start()
    {
        c_m.onClick.AddListener(m_choice);
        c_r.onClick.AddListener(r_choice);
    }

    void m_choice(){
        GAMEMANAGER.instance.player_is_reimu = false;
        loading.SetActive(true);
        gameObject.SetActive(false);
    }
    void r_choice(){
        GAMEMANAGER.instance.player_is_reimu = true;
        loading.SetActive(true);
        gameObject.SetActive(false);
    }

}
