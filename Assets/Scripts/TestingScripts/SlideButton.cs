using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200.0f))
            {
                if (hit.transform.gameObject == this.transform.gameObject)
                    Debug.Log("You clicked the " + hit.transform.name);
            }
        }
    }
}
