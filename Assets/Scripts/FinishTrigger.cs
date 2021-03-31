using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IAHostage>() && GameManager.Instance.isPhaseTwo)
        {
           GameManager.Instance.Win();
           Cursor.visible = true;
           Cursor.lockState = CursorLockMode.None;
        }
    }
}
