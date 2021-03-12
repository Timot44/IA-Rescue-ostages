using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : Player
{
    [Header("Parameters")]
    public float speed;
    public CharacterController controller;
    public float gravity = -9.6f;
    private Vector3 _velocity;

    private float moveX;

    private float moveZ;
    // Start is called before the first frame update
    private void Awake()
    {
        walkSpeed = speed;
    }

    void Start()
    {
        
    }

    public override void Movement()
    {
        Vector3 move = (transform.right * moveX) + (transform.forward * moveZ);
        
        controller.Move(move * (walkSpeed * Time.deltaTime));
        
    }

    // Update is called once per frame
    void Update()
    {
        //Je récupère le float de l'axis horizontal et vertical
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

       
    }

    private void FixedUpdate()
    {
        Movement();
    }
}
