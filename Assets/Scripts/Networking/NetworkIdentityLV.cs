using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkIdentityLV : NetworkBehaviour
{
    [SyncVar]
    public int modelIndex = 0;
    public GameObject dinos;

    [SerializeField]
    GameObject PlayerPrefab;

    [Command]
    void CmdSendModelIdxToServer(int idxToSend) 
    {
        RpcUpdatePlayerModel(idxToSend);
    }

    [ClientRpc]
    void RpcUpdatePlayerModel(int idx)
    {
        dinos.transform.GetChild(idx).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;
    }
}
