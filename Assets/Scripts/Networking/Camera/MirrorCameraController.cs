using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class MirrorCameraController : NetworkBehaviour
{

    public MirrorEnergy script;
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

    void Update()
    {

        if (script.isDead) {
            // Sets a flag that the player has died -> trigger UI for death
            Debug.Log("Player died");

            // Animates the camera to go into bird's eye view
            var playerCam = transform.Find("PlayerCinamachineTest");
            var transposer = playerCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset = new Vector3(0, 10, -5);
            // TODO Smooth out the transition between follow offsets 
            // possibly with a series height changes with delays in between
            // ideally lasting the same as the character death animation

            // TODO Animate the camera to circle around playing area

            // Prevents the update function from running endlessly after player death
            enabled = false;
        }
    }

}
