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
        health = maxHealth;
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
        Debug.DrawRay(transform.position,15*transform.forward,Color.green);
        Debug.DrawRay(transform.position,15*transform.forward-transform.right*4,Color.magenta);
        Debug.DrawRay(transform.position,15*transform.forward+transform.right*4,Color.red);
    }
}