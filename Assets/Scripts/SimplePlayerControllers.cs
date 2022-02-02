using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Need this
using Photon.Pun;

public class SimplePlayerControllers : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    // Need this
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        PrintInstruction();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine) 
        {
            MovePlayer();
        }
    }

    void PrintInstruction() 
    {
        Debug.Log("Use WASD to move");
    }

    void MovePlayer() 
    {
        float xVal = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zVal = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate(xVal, 0f, zVal);
    }
}
