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
    private Vector3 _velocity;

    private float moveX;

    private float moveZ;
    // Start is called before the first frame update
    private void Awake()
    {
      
    }

    void Start()
    {
        
    }

    //Function for moving the player
    private void Movement()
    {
        Vector3 move = (transform.right * moveX) + (transform.forward * moveZ);
        controller.Move(move * (speed * Time.deltaTime));
    }

    // Update is called once per frame
    void Update()
    {
        //I took the float of the horizontal axis and the vertical axis
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

       
    }

    private void FixedUpdate()
    {
        Movement();
    }
}
