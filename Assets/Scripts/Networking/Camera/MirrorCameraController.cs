using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class MirrorCameraController : NetworkBehaviour
{
    public override void OnStartClient()
    {
        if(!isLocalPlayer) {
            // Sets camera to be only renderable camera for client
            // var camera = transform.Find("PlayerCam");
            var camera = transform.Find("Camera");
            camera.GetComponent<Camera>().enabled = false;
            camera.GetComponent<CinemachineBrain>().enabled = false;
            camera.GetComponent<AudioListener>().enabled = false;

            var cinemachine = transform.Find("PlayerCinamachineTest");
            cinemachine.GetComponent<CinemachineVirtualCamera>().enabled = false;
        }

        if(isLocalPlayer) {
            // Sets UI elements to be only renderable elements for client
            var UI1 = transform.Find("Energy");
            var UI2 = transform.Find("EatContextMenu");
            UI1.GetComponentInChildren<Canvas>().enabled = true;
            UI2.GetComponentInChildren<Canvas>().enabled = true;
        }
    }

    // TODO:
    // - Get isDead value from MirrorEnergy
    // - Animate the camera going into birdseye view
    // - Set a flag that player has died and that it should open the UI for death
    // Debug.log("Prompt")
    // - Switch to focus on the map itself, and orbit around
}
