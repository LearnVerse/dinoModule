 using UnityEngine;

//Inspired by Comp-3 Unity Tutorials: https://www.youtube.com/channel/UC26kmK523wCy9RziFrvhp7g

[RequireComponent(typeof(CapsuleCollider))]
// float m_ScaleX, m_ScaleY, m_ScaleZ;
// m_ScaleX = .1f;
// m_ScaleY = .1f;
// m_ScaleZ = .1f;


public abstract class Interactable : MonoBehaviour
{     
    // public GameObject interactIcon;
    public CapsuleCollider cap;
    Vector3 center = new Vector3(0f,0f,0f);
    private float radius = 2.5f;
    private float height = 3f;
    private int dir = 1;

    public Canvas icon;
    public GameObject box;
    void Start()
    {
        box = GameObject.FindWithTag("Player");
        if(box != null)        
        {
            Debug.Log("found the box");
            icon = box.GetComponent<Canvas>();
        }
        cap = cap.GetComponent<CapsuleCollider>();
        cap.center = center;
        cap.radius = radius;
        cap.height = height;
        cap.direction = dir;
        
        cap.isTrigger = true; //set the collider to always be active for the interactable object
        // interactIcon = interactIcon.GetComponent<InteractControl>().GetGameObject();
    
    }

    public abstract void Interact(); //abstract method to be implemented for each type of interactable object (in our case, trees and carcasses)

    private void OnTriggerEnter(Collider collision) //method to display the "interact icon" upon detection of player entering the collider
    {
        if(collision.CompareTag("Player"))
            collision.GetComponent<InteractControl>().OpenInteractableIcon();
    }

    private void OnTriggerExit(Collider collision) //method to hide the "interact icon" upon detection of player entering the collider
    {
        if(collision.CompareTag("Player"))
            collision.GetComponent<InteractControl>().CloseInteractableIcon();
    }
}

