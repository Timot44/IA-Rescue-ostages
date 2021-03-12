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
    public override void SwitchToState(int stateIndex)
    {
        currentState = new DistStateP2(this);
    }
}