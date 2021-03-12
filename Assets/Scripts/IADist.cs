using UnityEngine.AI;

public class IADist : IAParent
{
    public DistState currentState;

   public NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = new DistStateP1(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Move();
    }
    public override void SwitchToState(int stateIndex)
    {
        currentState = new DistStateP2(this);
    }
}