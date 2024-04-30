using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public float rotationSpeed = 90f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the object around the y-axis at 'rotationSpeed' degrees per second.
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
