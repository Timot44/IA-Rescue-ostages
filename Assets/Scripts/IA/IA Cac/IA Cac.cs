using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACac : IAParent
{
    public float attackDistance;
    public float speedBoost;
    public int damageBoost;

    public CacState currentState;
    // Start is called before the first frame update
    public override void SwitchToState(int stateIndex)
    {
        switch (stateIndex)
        {
            case 1:
                currentState = new CacStateP1();
                break;
            case 2:
                currentState = new CacStateP2();
                break;
            default:
                Debug.LogError("State index do not exist : " + stateIndex);
                break;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
}
