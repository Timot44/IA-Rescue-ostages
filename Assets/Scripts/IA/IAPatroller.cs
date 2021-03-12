using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPatroller : IAParent
{
    private bool minePlaced;
    
    public int callEnemyCount;
    public float callEnemyMaxDistance;
    
    private bool fleeing;

    private PatrollerState currentState;


    void Flee()
    {
        
    }

    void CallEnemies()
    {
        
    }

    public override void SwitchToState(int stateIndex)
    {
        base.SwitchToState(stateIndex);
    }

    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
