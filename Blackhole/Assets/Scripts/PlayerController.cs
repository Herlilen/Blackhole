using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    
    [Header("Movement Control")] 
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float turnSpeed = 200f;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //ANIMATION SETTING//
        _animator.SetFloat("vertical", Input.GetAxis("Vertical"));
        _animator.SetFloat("horizontal", Input.GetAxis("Horizontal"));
    }

    private void OnAnimatorMove()
    {
        //CHARACTER CONTROLLER SETTING//
        Vector3 newPosition = transform.position + _animator.deltaPosition;
        quaternion newRotation = transform.rotation * _animator.deltaRotation;
        
        //do the rotation with a&d
        gameObject.transform.rotation = newRotation;
        
        //set movement through character controller
        _characterController.Move(_animator.deltaPosition);
    }
}
