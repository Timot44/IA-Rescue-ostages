using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor;
using UnityEngine;

public class IADist : IAParent
{
    public DistState currentState;

    public List<Transform> patrolPoints;
    // Start is called before the first frame update
    public override void SwitchToState(int stateIndex)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        currentState = new DistStateP1(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Move();
    }
}