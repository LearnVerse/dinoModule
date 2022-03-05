using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Custom Network Manager that inherits from Mirror Network Manager
public class NetworkManagerLV : NetworkManager
{
    public override void OnStartServer()
    {
        Debug.Log("Server Started!");
    }

    public override void OnStopServer()
    {
        Debug.Log("Server Stopped!");
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("Client connected");
    }
}
