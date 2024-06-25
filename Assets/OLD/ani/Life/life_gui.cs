using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life_gui : MonoBehaviour
{
    public GameManager manager;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.LIFE == 3){
            animator.Play("life_3");
        }
        else if(manager.LIFE == 2){
            animator.Play("life_2");
        }
        else if(manager.LIFE == 1){
            animator.Play("life_1");
        }
        else if(manager.LIFE == 0){
            animator.Play("life_0");
        }
    }
}
