using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openables1 : Interactable
{
    public GameObject eaten; 
    public Transform uneaten;
    

//ideas: use list of objects with meshRenderer and health values to show object being gradually eaten (more resource intensive)
        public override void Interact() //creates an "eaten bush" prefab clone at the position of the "uneaten bush" and hides the uneaten bush
        {
            Instantiate(eaten,uneaten.position, uneaten.rotation);
            gameObject.SetActive(false);
            if(icon!=null)
            {
                icon.enabled = false;
            }
        }
    


}
