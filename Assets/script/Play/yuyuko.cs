using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yuyuko : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BGMmanager.Instance.Sound.Stop();
        BGMmanager.Instance.Playsound("yuyuko_boss_play");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
