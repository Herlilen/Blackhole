using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuLightControl : MonoBehaviour
{
    public Light light;
    public float rotationSpeed = 45f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
