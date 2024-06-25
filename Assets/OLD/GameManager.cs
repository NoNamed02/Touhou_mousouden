using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro 네임스페이스 추가
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance; // 싱글톤을 할당할 전역 변수
    public int boss_spwan = 0;

    public bool isGameover = false; // 게임 오버 상태
    //public GameObject gameoverUI;

    public int LIFE = 3;

    public int score = 0;

    public bool re = false;
    public GameObject Player_spwan;
    
    public int spellCard_count = 3;
    
    public bool scene_check = false;

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
        GameObject Player_spwan_check = GameObject.FindGameObjectWithTag("spwan");
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        
        if(isGameover && Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Player_spwan_check == null && Player == null && LIFE > 0 && scene_check == false){
            LIFE -= 1;
            if(LIFE > 0){
                Instantiate(Player_spwan,new Vector3(0,-3.6f,-7), Quaternion.identity);
                spellCard_count = 3;
            }
            else if(LIFE == 0){
                StartCoroutine(ReturnToMainMenu());
            }
        }
    }
    
    
    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(3.0f); // 3초 대기
        boss_spwan = 0;

        isGameover = false; // 게임 오버 상태

        LIFE = 3;

        score = 0;

        re = false;
        
        spellCard_count = 3;
        
        scene_check = false;

        SceneManager.LoadScene("PlayScenes");
    }
}
