using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MirrorCameraController : NetworkBehaviour
{
    public override void OnStartClient()
    {
        if(!isLocalPlayer) {
            // Sets camera to be only renderable camera for client
            // var camera = transform.Find("PlayerCam");
            var camera = transform.Find("PlayerCinamachineTest");
            camera.GetComponent<Camera>().enabled = false;
            camera.GetComponent<AudioListener>().enabled = false;
        }

        if(isLocalPlayer) {
            // Sets UI elements to be only renderable elements for client
            var UI1 = transform.Find("Energy");
            var UI2 = transform.Find("EatContextMenu");
            UI1.GetComponentInChildren<Canvas>().enabled = true;
            UI2.GetComponentInChildren<Canvas>().enabled = true;
        }
    }
}
