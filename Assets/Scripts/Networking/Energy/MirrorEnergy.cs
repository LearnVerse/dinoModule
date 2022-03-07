
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Mirror;
/*written by: Alex Davis and Katrina Yu
CS98 Project: Team Learnverse
This script implements the Energy mechanic
*/
public class MirrorEnergy : NetworkBehaviour {
    public MirrorPlayerController ctrl; //player movement component
    // public MirrorCameraController camCtrl;
    public Image fill;
    public TextMeshProUGUI amount;
    private bool drainTrigger = false;
    public bool isDead = false;
    private Vector3 _prevPosition;
    private Vector3 _currPosition;
    private UIManager manager;
    public Animator anim;
    public Vector3 prevPosition //stores previous position of the Dino
    {
        get{return _prevPosition;}
        set{_prevPosition = value;}
    }    
    public AudioSource dyingSound;
    public AudioSource eatingSound;
    public AudioSource backgroundSound;

    public Vector3 currPosition //stores current position of the Dino
    {
        get{return _currPosition;}
        set{_currPosition = value;}
    }
    public GameObject dino; //your dino 

    public float currentValue, maxValue;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<UIManager>();
        fill.fillAmount = Normalise();
        int showNum = (int)Mathf.Floor(currentValue);
        amount.text = $"{showNum}%";
        prevPosition = dino.transform.position; //this is meant to refer to the child of this component that has a transform component . . . work in progress
        // UnityEngine.Debug.Log("Started");
        StartCoroutine(MovingDrain());
        if(isLocalPlayer) {
            backgroundSound.Play();
        }
    }

    void Update()
    {
        if(!drainTrigger){//if draintrigger hasnt been triggered
            InitMoveCheck();//check for initial movement
        }
    }
    
    public void UpdateUI()
    {
        fill.fillAmount = Normalise();
        int showNum = (int)Mathf.Floor(currentValue);
        amount.text = $"{showNum}%";
    }

    private void InitMoveCheck()
    {
        currPosition = dino.transform.position;
        //check if previous position matches current position
        if ((prevPosition - currPosition).sqrMagnitude  > 0.001f){//if moved . . .
            drainTrigger = true;//trigger energy drain
            // UnityEngine.Debug.Log("started moving: beginning base drain");
            StartCoroutine(BaseDrain());
        }
    }

    public IEnumerator Replenish_Energy()
    {
        if(isLocalPlayer){
            Add(10);

            float duration = 3.8f;

            if(GetComponent<InteractControl>().isMeatEater) duration = 4.8f;
            else duration = 3.8f;

            // Trigger eating animation
            anim.SetBool("isEating", true);
            eatingSound.Play();
            float tempMove = ctrl.moveSpeed;
            float tempRotate = ctrl.rotateSpeed;
            ctrl.moveSpeed=0f;
            ctrl.rotateSpeed=0f;
        
            yield return new WaitForSeconds(duration);
            anim.SetBool("isEating", false);  
            ctrl.moveSpeed=tempMove;
            ctrl.rotateSpeed=tempRotate;
        }
    }

    // public void Eat()
    // {
    //     if(isLocalPlayer) {
    //         anim.SetBool("isEating", !anim.GetBool("isEating"));
            
    //     }
        
    //     // RpcEat();
    // }
    // [ClientRpc]
    // public void RpcEat()
    // {
    //     anim.SetBool("isEating", !anim.GetBool("isEating"));
    // }

    
    public void Death()
    {
        // UnityEngine.Debug.Log("Successful death, yaaay~!");
        if(currentValue == 0){
            //TODO: trigger death animation here

            //freeze player's position
            // dino.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            ctrl.moveSpeed=0f;
            ctrl.rotateSpeed=0f;
            isDead = true;

            // Activate the Out of Energy UI and disable the energy bar
            if (isLocalPlayer) {
                manager.OutOfEnergy.GetComponent<Canvas>().enabled = true;
                manager.EnergyBar.GetComponent<Canvas>().enabled = false;
                dyingSound.Play();
                backgroundSound.mute = true;
            }

            // GetComponent<AnimationController>().dead = true;
            anim.SetBool("isDead", true);
        }
    }

    IEnumerator BaseDrain()
    {        
        // UnityEngine.Debug.Log("base drain starting");
        while(!isDead){
            Sub(1);
            if(currentValue>0){
            yield return new WaitForSeconds(2);
            }
            else{
                isDead = true;
                // UnityEngine.Debug.Log("dying, stopping drains");
                Death();
                StopAllCoroutines();
            }
        }
    }
    
    IEnumerator MovingDrain()
    {
        // UnityEngine.Debug.Log("entered moving drain");
        while(!isDead){
            currPosition = dino.transform.position;

            // UnityEngine.Debug.Log($"{prevPosition},{currPosition}");
            yield return new WaitForSeconds(0.5f);
            if((prevPosition - currPosition).sqrMagnitude  > 0.001f){//if moved . . .
                // UnityEngine.Debug.Log("is moving");
                // TODO: Balance Drain Value (should last at least a min)
                Sub(1.5f);//trigger energy drain
                if(currentValue>0){
                    yield return null;
                }
                else{
                    isDead = true;
                    // UnityEngine.Debug.Log("dying, stopping drains");                   
                    Death();
                    StopAllCoroutines();
                } 
            }
            // else UnityEngine.Debug.Log("stopped moving");           
            prevPosition = currPosition;
        }
    }
    private void Sub(float val)
    {
        // currentValue = Mathf.Min(currentValue+val,maxValue);
        currentValue -= val;

        if (currentValue> maxValue)
            currentValue = maxValue;
        if (currentValue<0)
            currentValue = 0;

        UpdateUI();
    }
   private void Add(int val)
    {
        // currentValue = Mathf.Min(currentValue+val,maxValue);
        currentValue += val;

        if (currentValue> maxValue)
            currentValue = maxValue;
        if (currentValue<0)
            currentValue = 0;

        UpdateUI();
    }


    private float Normalise()
    {
        return (float)currentValue/maxValue;
    }
    
}
