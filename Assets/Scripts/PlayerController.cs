﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float gravity = 9.8f;
    public float jumpForce;
    public float speed;

    public Animator animator;

    private Vector3 _moveVector;
    private float _fallVelocity = 0;

    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
       _characterController = GetComponent<CharacterController>(); 
    }
    void Update()
    {
        // Movement
        _moveVector = Vector3.zero;
        var runDirection = 0; 

        if (Input.GetKey(KeyCode.W))
        {
            _moveVector += transform.forward;
            runDirection = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _moveVector -= transform.forward;
            runDirection = 2;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _moveVector += transform.right;
            runDirection = 3;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _moveVector -= transform.right;
            runDirection = 4;
        }
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
        {
            _fallVelocity = -jumpForce;
        }

        animator.SetInteger("run direction", runDirection);
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        // Movement
        _characterController.Move(_moveVector * speed * Time.fixedDeltaTime);
        // Fall and jump
        _fallVelocity += gravity * Time.fixedDeltaTime;
        _characterController.Move(Vector3.down * _fallVelocity * Time.fixedDeltaTime);
        //Stop fall if on the ground
        if(_characterController.isGrounded)
        {
            _fallVelocity = 0;
        }
    }
}
