using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;

public class OrbitCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += Time.deltaTime * 0.1f;
    }
}
