using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IACac : IAParent
{
    public float attackDistance;
    public float speedBoost;
    public int damageBoost;
    public bool isPlayerDetected;
    public CacState currentState;
    
    
    public NavMeshAgent agent;
    public Transform player;
    // Start is called before the first frame update
    public override void SwitchToState()
    {
       currentState = new CacStateP2();
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new CacStateP1();
    }

    // Update is called once per frame
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
        
        Vector3 iAPos = transform.position;
        Vector3 iAForward = transform.forward * detectionRange;
        Vector3 iARight = transform.right * 3f;
        Ray ray1 = new Ray(iAPos, iAForward);
        Ray ray2 = new Ray(iAPos,iAForward - iARight);
        Ray ray3 = new Ray(iAPos,iAForward + iARight);
        Ray[] rays = new[] {ray1, ray2, ray3};
        RaycastHit hit;
        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, out hit, detectionRange))
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.Log(hit.collider.name);
                    isPlayerDetected = true;
                    return true;
                }
            }
        }
        isPlayerDetected = false;
        return false;
        
        
    }
    
    
    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position,detectionRange * transform.forward, Color.red);
        Debug.DrawRay(transform.position,detectionRange * transform.forward - transform.right * 3, Color.blue);
        Debug.DrawRay(transform.position,detectionRange * transform.forward + transform.right * 3, Color.green);
    }
    
}
