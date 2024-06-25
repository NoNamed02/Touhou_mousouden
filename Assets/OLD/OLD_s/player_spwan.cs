using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_spwan : MonoBehaviour
{
    public GameObject player;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource re = GetComponent<AudioSource>();
        re.Play();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3f);
        GameObject newPlayer = Instantiate(player, new Vector3(0, -3.6f, -7), Quaternion.identity);
        Player_move newPlayerMove = newPlayer.GetComponent<Player_move>();
        newPlayerMove.js = FindObjectOfType<bl_Joystick>();
        newPlayerMove.ResetHPBar(); // 새로운 플레이어의 HP 바를 초기화
        //newPlayerMove.Resetlife(); // 새로운 플레이어의 생명수를 초기화
        Destroy(gameObject);
    }
    /*
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3f);
        GameObject newPlayer = Instantiate(player, new Vector3(0, -3.6f, -7), Quaternion.identity);
        Player_move newPlayerMove = newPlayer.GetComponent<Player_move>();
        newPlayerMove.js = FindObjectOfType<bl_Joystick>();
        newPlayerMove.ResetHPBar(); // 새로운 플레이어의 HP 바를 초기화
        Destroy(gameObject);
    }
    */
}
