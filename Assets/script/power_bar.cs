using UnityEngine;
using UnityEngine.UI;

public class power_bar : MonoBehaviour
{
    public Slider hpSlider;
    public Player_move_reimu Player_move_2rd;

    void Start()
    {
        Player_move_2rd = FindObjectOfType<Player_move_reimu>();
        
        hpSlider.maxValue = 30;
    }

    void Update()
    {
        hpSlider.value = Player_move_2rd.level;
    }
}
