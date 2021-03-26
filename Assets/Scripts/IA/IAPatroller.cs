using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IAPatroller : IAParent
{
    private bool minePlaced;
    
    [Header("Call parameters")]
    public int callEnemyCount;
    public float callEnemyMaxDistance;
    
    [Header("Behavior")]
    public bool fleeing;
    public float timeToStopFleeing;

    private PatrollerState currentState;
    
    public NavMeshAgent agent;

    public Transform player;

    void Flee()
    {
        fleeing = true;
        StartCoroutine(WaitToStopFleeing());
        CallEnemies();
    }

    void CallEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        int helpCounter = callEnemyCount;

        foreach (var enemy in allEnemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < callEnemyMaxDistance)
            {
                enemy.GetComponent<IAParent>().HelpPatroller();
                helpCounter--;
            }
        }
    }

    public override void SwitchToState(int stateIndex)
    {
        switch (stateIndex)
        {
            case 1:
                currentState = new PatrollerStateP1();
                break;
            case 2:
                currentState = new PatrollerStateP2();
                break;
            default:
                Debug.LogError("State index do not exist : " + stateIndex);
                break;
        }
    }
    
    

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new PatrollerStateP1();
    }
    void Update()
    {
        currentState.Move(this);

        if (DetectPlayer())
        {
            Flee();
        }
    }

    public bool DetectPlayer()
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
                    return true;
                }
            }
        }
        return false;
    }

    public IEnumerator WaitToStopFleeing()
    {
        yield return new WaitForSeconds(timeToStopFleeing);
        fleeing = false;
    }

    public override void HelpPatroller()
    {
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position,detectionRange * transform.forward, Color.red);
        Debug.DrawRay(transform.position,detectionRange * transform.forward - transform.right * 3, Color.red);
        Debug.DrawRay(transform.position,detectionRange * transform.forward + transform.right * 3, Color.red);
    }
}
