using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marisa_ani : MonoBehaviour
{
    public Animator animator;

    public int ani = 1;
    void Start()
    {
        animator.GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger("ani", ani);
    }
}
