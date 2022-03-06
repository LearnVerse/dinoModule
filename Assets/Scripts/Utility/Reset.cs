using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Reset : NetworkBehaviour {
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.L)) {
            resetFood();
        }
    }

    [Command]
    public void resetFood()
    {
        RpcResetFood();
    }
    [ClientRpc]
    public void RpcResetFood()
    {
        if(true) Debug.Log("made it here");
        foreach(Transform foodt in transform){ //for each food item in this gameobject
            Interactable food = foodt.gameObject.GetComponent<Interactable>(); 
            Debug.Log($"{food}");
            //reset all their states to initial
            food.state = 0;
            food.mf.sharedMesh = food.states[food.state];
            food.mr.material = food.skins[food.state];
            food.wasTriggered = false;
        }
    }
}
