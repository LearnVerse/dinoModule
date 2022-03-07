using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UIManager : NetworkBehaviour
{
    public MirrorPlayerController ctrl;
    float tempMove;
    float tempRotate;
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
            ctrl.gravity = 0f;
            tempMove = ctrl.moveSpeed;
            tempRotate = ctrl.rotateSpeed;
            ctrl.moveSpeed=0f;
            ctrl.rotateSpeed=0f;
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
    public void CmdSetAnimator(bool dino)
    {
        RpcSetNewAnimator(dino);
    }

    [ClientRpc]
    public void RpcSetNewAnimator(bool dino)
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
            CmdSetAnimator(true);
            player.GetComponent<NetworkIdentityLV>().CmdSendModelIdxToServer(0);

            SelectDino.GetComponent<Canvas>().enabled = false;  
            
            ctrl.moveSpeed=tempMove;
            ctrl.rotateSpeed=tempRotate;
            ctrl.gravity = -9.81f;
        }        
    }

    public void SelectRexxy()
    {
        if(isLocalPlayer) {
            RexxyModel.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;

            player.GetComponent<InteractControl>().isMeatEater = true;
            CmdSetAnimator(false);
            player.GetComponent<NetworkIdentityLV>().CmdSendModelIdxToServer(1);

            SelectDino.GetComponent<Canvas>().enabled = false;
            
            ctrl.moveSpeed=tempMove;
            ctrl.rotateSpeed=tempRotate;
            ctrl.gravity = -9.81f;
        }
    }    
}
