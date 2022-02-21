using UnityEngine;
using System.Collections;

public class EnergyTest : MonoBehaviour
{
    public Energy en;

    public void StartAdd() 
    {
        StartCoroutine(AddDelay());
    }
    IEnumerator AddDelay()
    {
        for (int a = 0; a<10; a++){
            Add10();
            yield return new WaitForSeconds(3);
        }
    }
    public void Add10()
    {
        en.Add(10);
    }
}
