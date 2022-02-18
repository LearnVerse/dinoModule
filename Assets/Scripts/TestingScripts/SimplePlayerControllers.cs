using UnityEngine;
using Mirror;

public class SimplePlayerControllers : NetworkBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        // TODO: Store all of the current positions on the backend
        // When player moves, post to backend. Then backend tells frontend where to move.
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

