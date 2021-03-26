using System;
using UnityEngine;
using UnityEngine.AI;

public class IADist : IAParent
{
    public DistState currentState;

    public NavMeshAgent agent;
    
    void Start()
    {
        currentState = new DistStateP1(this);
    }

    void Update()
    {
        currentState.Move();
    }
    
    public override void SwitchToState()
    {
        currentState = new DistStateP2(this);
    }

    public override void HelpPatroller(Transform playerPos)
    {
        agent.SetDestination(playerPos.position);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,5*transform.forward,Color.green);
        Debug.DrawRay(transform.position,5*transform.forward-transform.right*2,Color.magenta);
        Debug.DrawRay(transform.position,5*transform.forward+transform.right*2,Color.red);
    }
}