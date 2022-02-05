using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractControl : MonoBehaviour
{
    public GameObject interactIcon;

    private void Start()
    {
        interactIcon.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //check for interaction on 'E' key press
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
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,2f,Vector3.forward,0f); //2m sphere of influence around character

        if(hits.Length > 0)
        {
            foreach(RaycastHit rc in hits)
            {
                if(rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }

    }
}
