using UnityEngine;
using UnityEngine.UI;

public class yukari_HP : MonoBehaviour
{
    
    public Slider hpSlider;
    public yukari_boss yukari;

    void Start()
    {
        yukari = FindObjectOfType<yukari_boss>();
        
        hpSlider.maxValue = yukari.HP;
    }

    void Update()
    {
        hpSlider.value = yukari.HP;
    }
}
