using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider slider;
    public Player_move playerMove; 

    void Start()
    {
        slider.maxValue = playerMove.HP;
        slider.value = playerMove.HP;
    }

    void Update()
    {
        slider.value = playerMove.HP;
    }
}
