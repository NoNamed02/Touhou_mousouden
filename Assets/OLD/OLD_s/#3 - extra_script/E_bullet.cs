using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_bullet : MonoBehaviour
{
    //private float bulletdir;
    //int a = 0;
    //public int B_speed = 10;
    public int B_speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        //bulletdir = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //a++
        transform.Translate(Vector2.down * B_speed * Time.deltaTime, Space.Self);

        /*
        if(a > 200){
            Destroy(self);
        }
        */
        if (gameObject.transform.position.y <= -16)
        { // 거리 벌어지면 파괴
            Destroy(gameObject);
        }
    }
}
