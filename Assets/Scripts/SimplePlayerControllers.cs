using UnityEngine;
using Mirror;

public class SimplePlayerControllers : NetworkBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            MovePlayer();
        }
    }

    void MovePlayer() 
    {
        float xVal = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zVal = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate(xVal, 0f, zVal);
    }
}

