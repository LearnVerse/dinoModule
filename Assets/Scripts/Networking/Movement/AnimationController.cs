using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Mirror;

public class AnimationController : NetworkBehaviour 
{

    // To link Unity variable with React Plugin
    [DllImport("__Internal")]
    private static extern void Walking(bool isWalking);

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

    // Update is called once per frame
    void Update()
    {

        // Send info over to React
    #if !UNITY_EDITOR && UNITY_WEBGL
        Walking(walking);
    #endif

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
