using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class end_score : MonoBehaviour
{
    public TMP_Text scoreText2;
    // Start is called before the first frame update
    void Start()
    {
        scoreText2.text = "Score: " + GameManager.instance.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
