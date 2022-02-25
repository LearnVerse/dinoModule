 using UnityEngine;
 using System.Collections;
 
 public class CameraMove : MonoBehaviour {
     
     public Transform player;
     public float speed = 10f;
 
     private Vector3 dest;
 
     void Start() {
         dest = new Vector3(player.position.x + 4, 0, -10);
     }
     
     void Update () 
     {
         dest = new Vector3(player.position.x + 4, 0, -10);
         transform.position = Vector3.Lerp (transform.position, dest, speed * Time.deltaTime);
     }
 }