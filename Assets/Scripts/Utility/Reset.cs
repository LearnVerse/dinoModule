using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Reset : NetworkBehaviour 
{
    public GameObject Foodbox;
    Foodbox.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);//< supposed fix, WiP
    private void Update() {
        if(Input.GetKeyDown(KeyCode.L)) {
            resetFood();
            Debug.Log("made it here1");
        }
    }

    [Command]
    public void resetFood()
    {
        Debug.Log("made it here2");
        RpcResetFood();
    }
    [ClientRpc]
    public void RpcResetFood()
    {
        if(true) Debug.Log("made it here3");
        foreach(Transform foodt in Foodbox.transform){ //for each food item in this gameobject
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
