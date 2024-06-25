using UnityEngine;
using UnityEngine.UI;

public class yuyuko_HP : MonoBehaviour
{
    public Slider hpSlider;
    public yuyuko_boss yuyuko;

    void Start()
    {
        yuyuko = FindObjectOfType<yuyuko_boss>();
        
        hpSlider.maxValue = yuyuko.HP;
    }

    void Update()
    {
        hpSlider.value = yuyuko.HP;
    }
}
