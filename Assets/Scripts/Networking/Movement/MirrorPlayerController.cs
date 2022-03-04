using UnityEngine;
using Mirror;

public class MirrorPlayerController : NetworkBehaviour
{
    public CharacterController _charController;
    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;
    private float inputX;
    private float inputZ;
    private Vector3 v_movement;

    void Start()
    {
        
    }


    private void Update() 
    {
        if(isLocalPlayer)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) {
                Debug.Log("Walking");
                GetComponent<AnimationController>().walking = true;
            }
            if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) {
                Debug.Log("Not walking");
                GetComponent<AnimationController>().walking = false;
            }
            
            

            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        //input forward
        v_movement = _charController.transform.forward * inputZ;

        // char rotate
        _charController.transform.Rotate(Vector3.up * inputX * (rotateSpeed * Time.deltaTime));

        // char move
        _charController.Move(v_movement * moveSpeed * Time.deltaTime);
  
    }
}
