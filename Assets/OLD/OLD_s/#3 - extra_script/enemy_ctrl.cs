using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_ctrl : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "bullet"){
            Debug.Log("HIT**************************");
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "bullet")
        {
            Debug.Log("hit cheeck***************");
            Destroy(gameObject);
        }
    }


}
