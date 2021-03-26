using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<IAHostage>().SetStateToPhaseTwo();
    }
}
