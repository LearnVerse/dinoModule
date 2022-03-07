using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Reset : NetworkBehaviour 
{
    public GameObject Foodbox;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.L)) {
            resetFood();
            Debug.Log("made it here1");
        }
    }

    [Command(requiresAuthority=false)]
    public void resetFood()
    {
        foreach(Transform food in Foodbox.transform) {
            Interactable foodInteractable = food.gameObject.GetComponent<Interactable>();
            foodInteractable.state = 0;
            foodInteractable.wasTriggered = false;
        }
    }
}
