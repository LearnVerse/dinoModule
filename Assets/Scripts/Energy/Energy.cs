
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Energy : MonoBehaviour
{
    public Image fill;
    public TextMeshProUGUI amount;

    public int currentValue, maxValue;

    // Start is called before the first frame update
    void Start()
    {
        fill.fillAmount = Normalise();
        amount.text = $"{currentValue}/{maxValue}";
        // StartCoroutine(AddDelay);
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
