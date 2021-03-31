using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IACac : IAParent
{
    [Header("Settings")]
    public float speedBoost;
    public int damageBoost;
    public bool isAlreadyAttacked;
    public float timeBetweenAttack;
    
    public bool isPlayerDetected;
   
    public CacState currentState;
    
    
    public NavMeshAgent agent;
    

    
    public GameObject obj_spoted;
    
    
    public override void SwitchToState()
    {
        currentState = new CacStateP2(this);
    }


     void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        currentState = new CacStateP1();
        health = maxHealth;
        SetBarMax(maxHealth);
    }

     
    void Update()
    {

        if (IsPlayerDetected())
        {
            currentState.RunToPlayer(this);
            
        }
        else
        {
            currentState.Move(this);
            
        }
    }

    public bool IsPlayerDetected()
    {
        float anglePerRay = angle / rayCount;
        
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 rayForward = new Vector3(detectionRange * Mathf.Sin((Mathf.Deg2Rad * anglePerRay * i) - (angle/2) * Mathf.Deg2Rad + transform.localEulerAngles.y * Mathf.Deg2Rad), 
                                     0, 
                                     detectionRange * Mathf.Cos((Mathf.Deg2Rad * anglePerRay * i) - (angle/2) * Mathf.Deg2Rad + transform.localEulerAngles.y * Mathf.Deg2Rad));
            
            Ray ray = new Ray(transform.position, rayForward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, detectionRange))
            {
                if (hit.collider.tag == "Player")
                {
                    obj_spoted = hit.collider.gameObject;
                    
                    isPlayerDetected = true;
                    return true;
                }
            }
        }
        isPlayerDetected = false;
        return false;
    }
    public override void HelpPatroller(Transform playerPos)
    {
        agent.SetDestination(playerPos.position);
    }

    public void ResetAttack()
    {
        isAlreadyAttacked = false;
    }
    
    
    private void OnDrawGizmosSelected()
    {
        float anglePerRay = (angle/2) / (rayCount/2);
        
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 rayForward = transform.position + new Vector3(
                                     detectionRange *
                                     Mathf.Sin((Mathf.Deg2Rad * anglePerRay * i) - (angle / 2) * Mathf.Deg2Rad +
                                               transform.localEulerAngles.y * Mathf.Deg2Rad), 0,
                                     detectionRange *
                                     Mathf.Cos((Mathf.Deg2Rad * anglePerRay * i) - (angle / 2) * Mathf.Deg2Rad +
                                               transform.localEulerAngles.y * Mathf.Deg2Rad));
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, rayForward - transform.position);
        }
    }
    
}
