using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_sub : MonoBehaviour
{
    [SerializeField]private int HP = 1000;
    public GameObject boss;
    private boss bossScript;
    private int frameCounter = 0;
    private int framesPerAction = 400; // n프레임마다 실행
    // Start is called before the first frame update
    
    public GameObject bulletPrefab;
    void Start()
    {
        bossScript = GameObject.FindGameObjectWithTag("boss").GetComponent<boss>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("boss");
        if (playerObject == null)
        {
            Destroy(gameObject);
        }
        else if(playerObject != null){
            
        }
        frameCounter++;
        if(frameCounter >= framesPerAction){//n프레임 마다 한번
            StartCoroutine(CircleBullets());
            frameCounter = 0;
        }

        if(HP < 0){
            bossScript.DecreaseHP(800); 
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player_bullet"))
        {
            
            HP -= 1;
        }
    }
    IEnumerator CircleBullets()
    {
        for(float i = 0f; i < 36f; i++){
            float angle = 0 + (10f*i);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(bulletPrefab, gameObject.transform.position, rotation);
        }
        yield return new WaitForSeconds(3.0f); 
    }
}
