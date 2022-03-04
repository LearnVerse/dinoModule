using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/*written by: Alex Davis
CS98 Project: Team Learnverse
This script is the main controller for the interactable objects
*/
public class InteractControl : NetworkBehaviour
//attach ui elements to player model
{
    public GameObject player;
    public Canvas interactIcon;
    public Canvas cantEatIcon;

    public float radiusOfInfluence = 3f;
    public MirrorEnergy en;
    public bool isMeatEater;

    [Client]
    public override void OnStartClient()
    {
        var masterGroup = transform.Find("Canvases");

        var tempIcon = masterGroup.Find("EatContextMenu");
        interactIcon = tempIcon.GetComponentInChildren<Canvas>();//sets the interactIcon's initial state to 'inactive' (invisible)
        tempIcon = masterGroup.Find("CantEatContextMenu");
        cantEatIcon = tempIcon.GetComponentInChildren<Canvas>();
        setIcon(player, true, false);
        setIcon(player, false, false);
    }
    // Update is called once per frame

    [Client]
    void Update()
    {
        //check for possible interaction and perform interaction on 'E' key press
        if(!hasAuthority){return; }
        if(Input.GetKeyDown(KeyCode.E))
            CheckInteraction();
    }

    [Command]
    public void setIcon(GameObject player, bool isInteractUI, bool state)
    {
        if(state) OpenIcon(player,isInteractUI);
        else CloseIcon(player,isInteractUI);
    }

    [TargetRpc]
    public void OpenIcon(GameObject player, bool isInteractUI)
    {
        Transform t = player.transform.Find("Canvases");
        var temp = t;
        if(isInteractUI) temp = t.Find("EatContextMenu");
        else temp = t.Find("CantEatContextMenu");
        Canvas icon = temp.GetComponentInChildren<Canvas>();
        if(icon == null) UnityEngine.Debug.Log("icon not found");
        else icon.enabled = true;
    }
    [TargetRpc]
    public void CloseIcon(GameObject player, bool isInteractUI)
    {
        Transform t = player.transform.Find("Canvases");
        var temp = t;
        if(isInteractUI) temp = t.Find("EatContextMenu");
        else temp = t.Find("CantEatContextMenu");
        Canvas icon = temp.GetComponentInChildren<Canvas>();
        if(icon == null) UnityEngine.Debug.Log("icon not found");
        else icon.enabled = false;
    }

    [Command]
    private void CheckInteraction()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,radiusOfInfluence,Vector3.forward,0f); //set public 3f "sphere of influence" around character
        if(hits.Length > 0)
        {
            foreach(RaycastHit rc in hits)
            {
                if(rc.transform.GetComponent<Interactable>())
                {
                    Interactable food = rc.transform.GetComponent<Interactable>().GetComponent<Interactable>();
                    bool check = food.wasTriggered;
                    if(check == false){
                        if (food.isMeat == isMeatEater){
                            Debug.Log(food);
                            food.Interact();
                            Debug.Log($"after:{food}");
                            en.StartCoroutine(en.Replenish_Energy());
                        } 
                        else UnityEngine.Debug.Log("Can't Eat this!");                       
                    }
                    interactIcon.enabled = false;
                    cantEatIcon.enabled = false;
                    return; //here, we use return in order to prevent multiple interactions at once (remove if we wish to implement AoE type interaction with any interactable object within range)
                }
            }
        }

    }
}
