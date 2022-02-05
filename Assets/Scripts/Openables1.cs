using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openables1 : Interactable
{
    public GameObject eatenBush;
    public Transform bush;
    public GameObject interactIcon;


        public override void Interact()
        {
            Instantiate(eatenBush,bush.position, bush.rotation);
            gameObject.SetActive(false);
            interactIcon.SetActive(false); 
        }
    


}
