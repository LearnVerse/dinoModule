// #define onlySyncOnChange_BANDWIDTH_SAVING
// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using Mirror;

// //Inspired by Comp-3 Unity Tutorials: https://www.youtube.com/channel/UC26kmK523wCy9RziFrvhp7g

// [RequireComponent(typeof(CapsuleCollider))]
// // float m_ScaleX, m_ScaleY, m_ScaleZ;
// // m_ScaleX = .1f;
// // m_ScaleY = .1f;
// // m_ScaleZ = .1f;


// public abstract class NetworkInteractionLV : NetworkBehaviour
// {     
//     public bool wasTriggered = false;
//     public abstract void Interact(); //abstract method to be implemented for each type of interactable object (in our case, trees and carcasses)
//     [TargetRpc]//we only want this to show up for this specific client
//     private void OnTriggerEnter(Collider collision) //method to display the "interact icon" upon detection of player entering the collider
//     {
//         if(collision.CompareTag("Player"))
//             if(wasTriggered == false) collision.GetComponent<InteractControl>().OpenInteractableIcon();
//     }
//     [TargetRpc]//we only want this to show up for this specific client
//     private void OnTriggerExit(Collider collision) //method to hide the "interact icon" upon detection of player entering the collider
//     {
//         if(collision.CompareTag("Player"))
//             collision.GetComponent<InteractControl>().CloseInteractableIcon();
//     }
// }