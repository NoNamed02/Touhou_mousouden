using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reimu_ani : MonoBehaviour
{
    public Animator animator;

    public int ani = 1;
    void Start()
    {
        animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("ani", ani);
    }
}
