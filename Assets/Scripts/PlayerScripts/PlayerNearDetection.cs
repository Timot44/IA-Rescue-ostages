using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearDetection : MonoBehaviour
{
    [SerializeField]
    private IAHostage ia;

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            ia.SetStateToPhaseTwo();
        }
        
    }
}
