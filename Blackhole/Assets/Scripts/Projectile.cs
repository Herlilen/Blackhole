using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float projIntForce;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(-transform.right * projIntForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rigidbody.velocity *= 0.995f;
    }
}
