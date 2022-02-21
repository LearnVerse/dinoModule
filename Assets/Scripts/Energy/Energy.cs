using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Energy : MonoBehaviour
{
    public Image fill;
    public TextMeshProUGUI amount;
    private bool isMoving = false; //checks if Dino is moving - false means stationary
    private bool drainTrigger = false;
    private Transform prevPosition; //stores last position of the Dino
    public GameObject dino; //your dino 
    private Transform currPosition; //stores current position of the Dino

    public int currentValue, maxValue;

    // Start is called before the first frame update
    void Start()
    {
        fill.fillAmount = Normalise();
        amount.text = $"{currentValue}/{maxValue}";
        prevPosition = prevPosition.GetComponentInChildren<GameObject>().transform; //this is meant to refer to the child of this component that has a transform component . . . work in progress
    }

    void Update()
    {
        if(!drainTrigger){
            MoveCheck();
        }


    }

    private void MoveCheck()
    {
        currPosition = currPosition.GetComponentInChildren<GameObject>().transform;

        // currPosition = currPosition.GetComponent<dino>().transform;
        //check if previous position matches current position
        if (prevPosition != currPosition){
            drainTrigger = true;
        }
        else{
            prevPosition = currPosition;
        }
    }
    public void Replenish_Energy()
    {
        //when player eats food

        //increase energy by 10
    }
    public void Death()
    {
        //when energy reaches 0

        //trigger Death animation

        //lock out player, send to sky view
    }
        public void Sub(int val)
    {
        // currentValue = Mathf.Min(currentValue+val,maxValue);
        currentValue -= val;

        //bounds check
        if (currentValue> maxValue)
            currentValue = maxValue;

        fill.fillAmount = Normalise();
        amount.text = $"{currentValue}/{maxValue}"; //update HUD element
        

    }
    public void Add(int val)
    {
        // currentValue = Mathf.Min(currentValue+val,maxValue);
        currentValue += val;

        if (currentValue> maxValue)
            currentValue = maxValue;

        fill.fillAmount = Normalise();
        amount.text = $"{currentValue}/{maxValue}";
        

    }



    private float Normalise()
    {
        return (float)currentValue/maxValue;
    }
    
}