using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{    
    [SerializeField]
    private Transform cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
