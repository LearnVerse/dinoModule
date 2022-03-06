using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkIdentityLV : NetworkBehaviour
{
    [SyncVar(hook="OnModelChange")]
    public int modelIndex;
    public GameObject dinos;

    [SerializeField]
    GameObject PlayerPrefab;

    [Command]
    public void CmdSendModelIdxToServer(int idxToSend) 
    {
        modelIndex = idxToSend;
        RpcUpdatePlayerModel(idxToSend);
    }

    [ClientRpc]
    void RpcUpdatePlayerModel(int idx)
    {
        modelIndex = idx;
        dinos.transform.GetChild(idx).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

    void OnModelChange(int oldValue, int newValue) 
    {
        dinos.transform.GetChild(newValue).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;
    }
}
