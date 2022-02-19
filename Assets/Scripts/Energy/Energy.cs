
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Energy : MonoBehaviour
{
    public Image fill;
    public TextMeshProUGUI amount;
    private bool isMoving; //checks if Dino is moving - false means stationary
    private Transform prevPosition; //stores last position of the Dino

    public int currentValue, maxValue;

    // Start is called before the first frame update
    void Start()
    {
        fill.fillAmount = Normalise();
        amount.text = $"{currentValue}/{maxValue}";
        // StartCoroutine(AddDelay);
    }

    void Update()
    {
        prevPosition = GameObject
    }

    public void Drain_Energy()
    {
        //when 3 seconds pass . . .

        //if player is stationary
        if(isMoving)
        //drain x energy

        //else, drain 2x energy                                                                                                 

    }

    private void MoveCheck()
    {
        //check if previous position matches current position
        if 
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
