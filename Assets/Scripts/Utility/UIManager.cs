using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UIManager : NetworkBehaviour
{

    public Canvas OutOfEnergy;
    public Canvas SelectDino;
    public Canvas EnergyBar;
    public GameObject player;
    public GameObject SteggyModel;
    public GameObject RexxyModel;
    [SerializeField]
    public RuntimeAnimatorController RexxyAnimator;
    [SerializeField]
    public RuntimeAnimatorController SteggyAnimator;
    [SerializeField]
    public Avatar RexxyAvatar;
    [SerializeField]
    public Avatar SteggyAvatar;

    public override void OnStartClient() 
    {
        if(isLocalPlayer) {
            SelectDino.GetComponent<Canvas>().enabled = true;
        }
    }

    public void TestButton()
    {
        Debug.Log("Button pressed");
    }

    public void BackButton()
    {
        if(isLocalPlayer) {
            OutOfEnergy.GetComponent<Canvas>().enabled = false;
        }
    }

    [Command]
    public void SetAnimator(bool dino)
    {
        Debug.Log(dino);
        SetNewAnimator(dino);
    }

    [ClientRpc]
    public void SetNewAnimator(bool dino)
    {
        if(dino) {
            player.GetComponent<Animator>().runtimeAnimatorController = SteggyAnimator;
            player.GetComponent<Animator>().avatar = SteggyAvatar;
            player.GetComponent<NetworkAnimator>().animator = player.GetComponent<Animator>();
        } else {
            player.GetComponent<Animator>().runtimeAnimatorController = RexxyAnimator;
            player.GetComponent<Animator>().avatar = RexxyAvatar;
            player.GetComponent<NetworkAnimator>().animator = player.GetComponent<Animator>();
        }        
    }

    public void SelectSteggy()
    {
        if(isLocalPlayer) {
            SteggyModel.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;

            player.GetComponent<InteractControl>().isMeatEater = false;
            Debug.Log("Selecting Steggy");
            SetAnimator(true);
            Debug.Log("Selected Steggy");

            player.GetComponent<NetworkIdentityLV>().CmdSendModelIdxToServer(0);

            SelectDino.GetComponent<Canvas>().enabled = false;  
        }        
    }

    public void SelectRexxy()
    {
        if(isLocalPlayer) {
            RexxyModel.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;

            player.GetComponent<InteractControl>().isMeatEater = true;
            
            Debug.Log("Selecting Rexxy");
            SetAnimator(false);
            Debug.Log("Selected Rexxy");

            player.GetComponent<NetworkIdentityLV>().CmdSendModelIdxToServer(1);

            SelectDino.GetComponent<Canvas>().enabled = false;
        }
    }

    
}
