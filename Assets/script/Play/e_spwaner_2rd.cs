using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e_spawn_2rd : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject pos;
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject pos4;
    public GameObject posEX;
    public talk_bg talk_Bg;

    private bool is_end = false;
    
    public int i = 0;
    public txtmanager txtmanager;
    void Start()
    {
    }

    void Update()
    {
        if(GAMEMANAGER.instance.game_start){
            int rand_n = Random.Range(1, 4);
            i++;
            if (i % 100 == 0 && GAMEMANAGER.instance.enemy_break_count < 60) // N체 격추까지 잡몹소환
            {
                if(rand_n == 1){
                    int randomNumber = Random.Range(-4, 4);
                    Vector3 newPosition = pos.transform.position + new Vector3(randomNumber, 0, 0);
                    Instantiate(enemy, newPosition, Quaternion.identity);
                }
                if(rand_n == 2){
                    int randomNumber = Random.Range(1, 5);
                    if(randomNumber == 1)
                        Instantiate(enemy2, pos1.transform.position, Quaternion.identity);
                    else if(randomNumber == 2)
                        Instantiate(enemy2, pos2.transform.position, Quaternion.identity);
                    else if(randomNumber == 3)
                        Instantiate(enemy2, pos3.transform.position, Quaternion.identity);
                    else if(randomNumber == 4)
                        Instantiate(enemy2, pos4.transform.position, Quaternion.identity);
                }
                if(rand_n == 3){
                    int randomNumber = Random.Range(2, -2);
                    Vector3 newPosition = posEX.transform.position + new Vector3(0.25f + randomNumber, 0, 0);
                    Instantiate(enemy2, newPosition, Quaternion.identity);
                    Vector3 newPosition2 = posEX.transform.position + new Vector3(-0.25f+ randomNumber, 0, 0);
                    Instantiate(enemy2, newPosition2, Quaternion.identity);
                    Vector3 newPosition3 = posEX.transform.position + new Vector3(0.25f+ randomNumber, 0.5f, 0);
                    Instantiate(enemy2, newPosition3, Quaternion.identity);
                    Vector3 newPosition4 = posEX.transform.position + new Vector3(-0.25f+ randomNumber, 0.5f, 0);
                    Instantiate(enemy2, newPosition4, Quaternion.identity);
                    Vector3 newPosition5 = posEX.transform.position + new Vector3(0.25f+ randomNumber, -0.5f, 0);
                    Instantiate(enemy2, newPosition5, Quaternion.identity);
                    Vector3 newPosition6 = posEX.transform.position + new Vector3(-0.25f+ randomNumber, -0.5f, 0);
                    Instantiate(enemy2, newPosition6, Quaternion.identity);
                }
            }
            else if(GAMEMANAGER.instance.enemy_break_count >= 60 && is_end == false){
                talk_Bg.gameObject.SetActive(true);
                is_end = true;
                GAMEMANAGER.instance.game_start = false;
                txtmanager.currentLine = 13;
                talk_Bg.back_st();
                txtmanager.DisplayNextSentence();
            }
            if(GAMEMANAGER.instance.enemy_break_count == 25){
                Vector3 newPosition = posEX.transform.position + new Vector3(0, 0.5f, 0);
                Instantiate(enemy2, newPosition, Quaternion.identity);
                Vector3 newPosition2 = posEX.transform.position + new Vector3(-0.5f, 0, 0);
                Instantiate(enemy2, newPosition2, Quaternion.identity);
                Vector3 newPosition3 = posEX.transform.position + new Vector3(0.5f, 0, 0);
                Instantiate(enemy2, newPosition3, Quaternion.identity);
                Vector3 newPosition4 = posEX.transform.position + new Vector3(1f, 0.5f, 0);
                Instantiate(enemy2, newPosition4, Quaternion.identity);
                Vector3 newPosition5 = posEX.transform.position + new Vector3(-1f, 0.5f, 0);
                Instantiate(enemy2, newPosition5, Quaternion.identity);
                GAMEMANAGER.instance.enemy_break_count++;
            }
        }
    }
    
}
