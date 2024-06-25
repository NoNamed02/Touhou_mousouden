using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reimu_bomb : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public bool is_big = false;
    public bool is_sub = false;
    public int SAFE_TIME = 500;
    private Transform trans;
    
    public Transform player; // 플레이어의 Transform
    void Start()
    {
        trans = GetComponent<Transform>();
        trans.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else if (player == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position;
        SAFE_TIME -= 1;
        if(is_big){
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            if(trans.localScale.x <= 1){
                Vector3 sub = trans.localScale;
                sub += new Vector3(0.02f, 0.02f, 0.02f);
                trans.localScale = sub;
            }
        }
        else{
            transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
            if(trans.localScale.x <= 0.5){
                Vector3 sub = trans.localScale;
                sub += new Vector3(0.01f, 0.01f, 0.01f);
                trans.localScale = sub;
            }
        }

        if(SAFE_TIME <= 0){
            if(is_sub == false)
                Soundmanager.Instance.Playsound("glass_break");
            Destroy(gameObject);
        }
    }
}
