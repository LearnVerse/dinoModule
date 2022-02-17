using System;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openables2 : Interactable
{
    // public [GameObject] eaten; 
    public List<Mesh> states;
    public List<Material> skins;
    
    private MeshFilter mf;
    private MeshRenderer mr;
    private int state = 0;


    
    private void Start()
    {
        mf = gameObject.GetComponent<MeshFilter>();
        mr = gameObject.GetComponent<MeshRenderer>();
        mf.sharedMesh = states[state];
    }

    //ideas: use list of objects with meshRenderer and health values to show object being gradually eaten (more resource intensive)
    public override void Interact() //creates an "eaten bush" prefab clone at the position of the "uneaten bush" and hides the uneaten bush
    {
        if(state < 1) 
        {
            state++;
            mf.sharedMesh = states[state];
            mr.material = skins[state];
        } 
    }
    


}