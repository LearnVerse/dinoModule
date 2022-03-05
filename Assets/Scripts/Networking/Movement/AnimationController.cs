using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class AnimationController : NetworkBehaviour 
{
    public Animator animator;

    public bool eating;
    public bool walking;
    public bool dead;

    // Start is called before the first frame update
    public override void OnStartClient()
    {
        eating = false;
        walking = false;
        dead = false;
    }

    void Update()
    {
        UpdateAnim();
    }

    void UpdateAnim()
    {
        if(walking) {
            animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
        }

        if(eating) {
            animator.SetBool("isEating", true);
        } else {
            animator.SetBool("isEating", false);
        }

        if(dead) {
            animator.SetBool("isDead", true);
        }
    }
}