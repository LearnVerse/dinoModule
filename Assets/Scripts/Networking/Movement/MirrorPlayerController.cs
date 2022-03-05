using UnityEngine;
using Mirror;

public class MirrorPlayerController : NetworkBehaviour
{
    public CharacterController _charController;
    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;
    private float inputX;
    private float inputZ;
    public Vector3 velocity;
    public Vector3 move;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    public float gravity = -9.81f;

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
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        move = transform.forward * inputZ;

        _charController.Move(move * moveSpeed * Time.deltaTime);

        // char rotate
        _charController.transform.Rotate(Vector3.up * inputX * (rotateSpeed * Time.deltaTime));

        velocity.y += gravity * Time.deltaTime;

        // char move
        _charController.Move(velocity * Time.deltaTime);
  
    }
}
