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
    [SyncVar(hook = nameof(SetEatingPref))]
    public bool isMeatEater;
    [SerializeField]
    private Animator anim;

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

    [Command]
    void SetEatingPref(bool oldPref, bool newPref) 
    {
        isMeatEater = newPref;
        RpcSetEatingPref(isMeatEater);
    }

    [ClientRpc]
    public void RpcSetEatingPref(bool state)
    {
        isMeatEater = state;
    }


    // Update is called once per frame
    private void Update()
    {
        //check for possible interaction and perform interaction on 'E' key press
        if(!hasAuthority){return; }
        if(Input.GetKeyDown(KeyCode.E)) {
            CheckInteraction();
        }
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
        var temp1 = t;
        var temp2 = t;
        if(isInteractUI){
            temp1 = t.Find("EatContextMenu");
            temp2 = t.Find("CantEatContextMenu");
        } 
        else{
            temp1 = t.Find("CantEatContextMenu");
            temp2 = t.Find("EatContextMenu");
        }
        Canvas icon1 = temp1.GetComponentInChildren<Canvas>();
        Canvas icon2 = temp2.GetComponentInChildren<Canvas>();
        if(icon1 == null) UnityEngine.Debug.Log("icon not found");
        else if(icon2.enabled == true) return;
        else icon1.enabled = true;
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

    private void CheckInteraction()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,radiusOfInfluence,Vector3.forward,0f); //set public 3f "sphere of influence" around character
        if(hits.Length > 0)
        {
            foreach(RaycastHit rc in hits)
            {
                if(rc.transform.GetComponent<Interactable>())
                {
                    Interactable food = rc.transform.GetComponent<Interactable>();
                    bool check = food.wasTriggered;
                    if(check == false){
                        if (food.isMeat == isMeatEater){                                                      
                            Debug.Log(food);//if eat > bool animation > send back to server food state
                            food.CmdInteract(1); //<handles sending food state to server
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
