using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAPatroller : IAParent
{
    private bool minePlaced;
    
    [Header("Call parameters")]
    public int callEnemyCount;
    public float callEnemyMaxDistance;
    
    private bool fleeing;

    private PatrollerState currentState;
    
    private NavMeshAgent agent;
    


    void Flee()
    {
        
    }

    void CallEnemies()
    {
        
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
    }
    void Update()
    {
        
    }
}
