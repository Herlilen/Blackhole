using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    //singleton setting
    private static Blackhole instance;
    
    //black hole settings 
    [Header("Black Hole Setting")]
    private Rigidbody _rigidbody;
    private Collider _collider;
    public List<GameObject> inbondObjects = new List<GameObject>(); //array of in-bonding objects
    public float gravitationalConstant = 6.674e-11f;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
    }

    private void FixedUpdate()
    {
        foreach (var inbondObject in inbondObjects.ToArray())
        {
            //get the rigidbody of the in-bond obj so get the mass
            Rigidbody inbondRigidbodyb = inbondObject.GetComponent<Rigidbody>();
            
            //calculate the distance between black hole and the objects
            float distance = Vector3.Distance(gameObject.transform.position, inbondObject.transform.position);
            
            //calculate the gravitational force
            float gforce = (gravitationalConstant * _rigidbody.mass * inbondRigidbodyb.mass) / (distance * distance);
            
            //calculate the direction of the force
            Vector3 forceDirection = (gameObject.transform.position - inbondObject.transform.position).normalized;
            
            //apply the gravitational force to the in-bond object
            inbondRigidbodyb.AddForce(forceDirection * gforce, ForceMode.Force);
            
            //absorb the object if distance < x
            if (distance <= .8)
            {
                _rigidbody.mass += inbondRigidbodyb.mass;
                inbondObjects.Remove(inbondObject);
                Destroy(inbondObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Projectile>() != null)
        {
            if (!inbondObjects.Contains(other.gameObject))
            {
                inbondObjects.Add(other.gameObject);
                Debug.Log("Object added to array: " + other.gameObject.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Projectile>() != null)
        {
            if (inbondObjects.Contains(other.gameObject))
            {
                inbondObjects.Remove(other.gameObject);
                Debug.Log("Object removed from array: " + other.gameObject.name);
            }
        }
    }
    
}
