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
            var masterGroup = transform.Find("Canvases");

            var UI1 = masterGroup.Find("EatContextMenu");
            var UI2 = masterGroup.Find("Energy");
            UI1.GetComponentInChildren<Canvas>().enabled = false;
            UI2.GetComponentInChildren<Canvas>().enabled = true;
        }
    }

    void Update()
    {

        if (script.isDead) {
            // Sets a flag that the player has died -> trigger UI for death
            Debug.Log("Player died");

            //perform camera transitions
            Debug.Log("Start camera coroutine");
            StartCoroutine(transitionCameras());

            // Prevents the update function from running endlessly after player death
            enabled = false;
        }

        IEnumerator transitionCameras(){
            // Animates the camera to go into bird's eye view
            var playerCam = transform.Find("PlayerCinamachineTest");
            var transposer = playerCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();

            // manually smooths transition to bird's eye view
            // possible alternate implementation: use another cinemachine camera instead of just
            // adjusting the height of the current one, then can use cinemachine blend
            float transitionTimeSec = 5f; //TODO: should set to be equal to death animation time
            float fps = 120f;
            float startY = 0f;
            float finalY = 10f;

            for(float currY = startY; currY < finalY; currY=currY+((finalY-startY)/(fps*transitionTimeSec))) {
                transposer.m_FollowOffset = new Vector3(0, currY, -5);
                yield return new WaitForSeconds(1f/fps);
            }

            // Turns off current camera, Automatically switches to camera surveying game area
            transform.Find("Camera").GetComponent<Camera>().enabled = false;
            Debug.Log("End camera coroutine");
        }
    }

}
