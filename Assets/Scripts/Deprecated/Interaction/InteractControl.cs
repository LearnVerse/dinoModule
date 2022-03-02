using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*written by: Alex Davis
CS98 Project: Team Learnverse
This script is the main controller for the interactable objects
*/
public class InteractControl : MonoBehaviour
//attach ui elements to player model
{
    private Canvas interactIcon;
    public float radiusOfInfluence = 3f;
    public Energy en;

    private void Start()
    {
        interactIcon = GameObject.FindGameObjectWithTag("UI1").GetComponent<Canvas>();   
        interactIcon.enabled = false; //sets the interactIcon's initial state to 'inactive' (invisible)
    }
    // Update is called once per frame
    void Update()
    {
        //check for possible interaction and perform interaction on 'E' key press
        if(Input.GetKeyDown(KeyCode.E))
            CheckInteraction();
    }



    public void OpenInteractableIcon()
    {
        interactIcon.enabled = true;
    }

    public void CloseInteractableIcon()
    {
        interactIcon.enabled = false;
    }

    private void CheckInteraction()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,radiusOfInfluence,Vector3.forward,0f); //set public 3f "sphere of influence" around character

        if(hits.Length > 0)
        {
            foreach(RaycastHit rc in hits)
            {
                if(rc.transform.GetComponent<Interactable>())
                {
                    bool check = rc.transform.GetComponent<Interactable>().GetComponent<Openables2>().wasTriggered;
                    if(check == false){
                        rc.transform.GetComponent<Interactable>().Interact();
                        en.Replenish_Energy();
                    }
                    interactIcon.enabled = false;
                    return; //here, we use return in order to prevent multiple interactions at once (remove if we wish to implement AoE type interaction with any interactable object within range)
                }
            }
        }

    }
}
