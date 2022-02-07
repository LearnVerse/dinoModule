using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*written by: Alex Davis
CS98 Project: Team Learnverse
This script is the main controller for the interactable objects
*/
public class InteractControl : MonoBehaviour
{
    public GameObject interactIcon;

    private void Start()
    {
        interactIcon = Instantiate(interactIcon);       
        interactIcon.SetActive(false); //sets the interactIcon's initial state to 'inactive' (invisible)
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
        interactIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,3f,Vector3.forward,0f); //2m "sphere of influence" around character

        if(hits.Length > 0)
        {
            foreach(RaycastHit rc in hits)
            {
                if(rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return; //here, we use return in order to prevent multiple interactions at once (remove if we wish to implement AoE type interaction with any interactable object within range)
                }
            }
        }

    }

    public GameObject GetGameObject()
    {
        Debug.Log("wahteifsay");
        return interactIcon;
    }
}
