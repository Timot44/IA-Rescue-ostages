using UnityEngine;
using UnityEngine.AI;

public class IADist : IAParent
{
    public DistState currentState;

    public NavMeshAgent agent;
    public Transform miradorTransform;
    
    public bool isRushing;
    public Vector3 rushPos;
    void Start()
    {
        currentState = new DistStateP1(this);
        health = maxHealth;
        SetBarMax(maxHealth);
    }

    void Update()
    {
        currentState.Move();
    }
    
    public override void SwitchToState()
    {
        Debug.Log("je change d'état");
        currentState = new DistStateP2(this);
    }

    public override void HelpPatroller(Transform playerPos)
    {
        isRushing = true;
        rushPos = playerPos.position;
        
    }
}