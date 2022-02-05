using UnityEngine;

//Inspired by Comp-3 Unity Tutorials: https://www.youtube.com/channel/UC26kmK523wCy9RziFrvhp7g

[RequireComponent(typeof(BoxCollider))]
// float m_ScaleX, m_ScaleY, m_ScaleZ;
// m_ScaleX = .1f;
// m_ScaleY = .1f;
// m_ScaleZ = .1f;
// Vector3 center = new Vector3(0f,0f,10f);
// float radius, height;
// int dir;

public abstract class Interactable : MonoBehaviour
{
    private void Reset() 
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
    public abstract void Interact();

    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.CompareTag("Player"))
            collision.GetComponent<InteractControl>().OpenInteractableIcon();
    }

    private void OnTriggerExit(Collider collision) 
    {
        if(collision.CompareTag("Player"))
            collision.GetComponent<InteractControl>().CloseInteractableIcon();
    }
}

