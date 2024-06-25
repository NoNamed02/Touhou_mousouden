using UnityEngine;
using UnityEngine.UI;

public class remilia_HP : MonoBehaviour
{
    public Slider hpSlider;
    public remilia remiliaScript;

    void Start()
    {
        remiliaScript = FindObjectOfType<remilia>();
        
        hpSlider.maxValue = remiliaScript.HP;
    }

    void Update()
    {
        hpSlider.value = remiliaScript.HP;
    }
}
