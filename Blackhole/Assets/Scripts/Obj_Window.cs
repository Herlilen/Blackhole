using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Window : MonoBehaviour
{
    [SerializeField] private BoxCollider trigger;
    public float sanityRevocer;
    public float concentrationRecover;
    public bool recovering;
    
    // Update is called once per frame
    void Update()
    {
        if (recovering)
        {
            PlayerStatus.instance.AddResource("SANITY", sanityRevocer * Time.deltaTime);
            PlayerStatus.instance.AddResource("CONCENTRATION", concentrationRecover * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            recovering = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            recovering = false;
        }
    }
}
