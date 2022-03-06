using System;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//Inspired by Comp-3 Unity Tutorials: https://www.youtube.com/channel/UC26kmK523wCy9RziFrvhp7g

[RequireComponent(typeof(CapsuleCollider))]
// float m_ScaleX, m_ScaleY, m_ScaleZ;
// m_ScaleX = .1f;
// m_ScaleY = .1f;
// m_ScaleZ = .1f;


public class Interactable : NetworkBehaviour
{     
    // Collider test;
    public List<Mesh> states;
    public List<Material> skins;
    
    public MeshFilter mf;
    public MeshRenderer mr;
    [SyncVar(hook = nameof(changeState))]    
    public int state = 0;
    public int finalState = 1; //todo make public
    public bool wasTriggered = false;
    public bool isMeat;
  
    [Command]
    public void Interact()
    {
        RpcInteract();
    }
    [ClientRpc]
    public void RpcInteract() //creates an "eaten bush" prefab clone at the position of the "uneaten bush" and hides the uneaten bush
    { 
        Debug.Log("reached interact");
        Debug.Log($"{state},{finalState}");
        if(state < finalState){
            Debug.Log("reached state logic");
            changeState(state,state+1);
            if(state == finalState) wasTriggered = true;
            mf.sharedMesh = states[state];
            mr.material = skins[state];
        } 
    }    
    void changeState(int oldState, int newState){
        Debug.Log($"{oldState},{newState}");
        state = newState;
    }
    // public void getInteract(){
    //     Interact();
    // }
    private void Start()
    {   
        mf = gameObject.GetComponent<MeshFilter>();
        mr = gameObject.GetComponent<MeshRenderer>();
        mf.sharedMesh = states[state];
    }

    private void OnTriggerEnter(Collider collision) //method to display the "interact icon" upon detection of player entering the collider
    {
        if (wasTriggered == false){
            if(collision.CompareTag("Player"))
                UnityEngine.Debug.Log("is a player");
                InteractControl playerctrl = collision.GetComponent<InteractControl>(); 
                if(playerctrl == null) UnityEngine.Debug.Log("player interact controller not found");
                if(playerctrl.isMeatEater != isMeat) playerctrl.setIcon(playerctrl.player, false, true);
                else playerctrl.setIcon(playerctrl.player, true, true);            
                UnityEngine.Debug.Log("open interact icon");
        }


    }

    private void OnTriggerExit(Collider collision) //method to hide the "interact icon" upon detection of player entering the collider
    {
        if(collision.CompareTag("Player"))
            UnityEngine.Debug.Log("is a player");
            InteractControl playerctrl = collision.GetComponent<InteractControl>();
            if(playerctrl == null) UnityEngine.Debug.Log("player interact controller not found");
            else{
                playerctrl.setIcon(playerctrl.player, true, false);
                playerctrl.setIcon(playerctrl.player, false, false);
            }
            UnityEngine.Debug.Log("close interact icon");
    }
}

