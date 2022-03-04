using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas OutOfEnergy;
    public Canvas SelectDino;
    public Canvas EnergyBar;

    public void TestButton()
    {
        Debug.Log("Button pressed");
    }

    public void BackButton()
    {
        OutOfEnergy.GetComponent<Canvas>().enabled = false;
        SelectDino.GetComponent<Canvas>().enabled = false;
    }
}
