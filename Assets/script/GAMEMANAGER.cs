using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro 네임스페이스 추가
using System.Collections;

public class GAMEMANAGER : MonoBehaviour {
    public static GAMEMANAGER instance; // 싱글톤을 할당할 전역 변수
    public int LIFE = 4;
    public int score = 0;
    public int spellCard_count = 4;
    public bool game_start = false;
    public int enemy_break_count = 0;

    public bool player_is_reimu = true;
    void Awake() {
        DontDestroyOnLoad(gameObject);
        if (instance == null){
            instance = this;
        }
        else{
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }
    void Update() {
        /*
        if(LIFE <= 0)
            game_start = false;
        */
    }
}
