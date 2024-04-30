using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceUIOrientation : MonoBehaviour
{
    public Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
        {
            //look at the camera
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }
    }
}
