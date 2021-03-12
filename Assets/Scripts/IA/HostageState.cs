using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostageState
{
    public virtual void Move()
    {
        
    }
}

public class HostageStateP1 : HostageState
{
    
}

public class HostageStateP2 : HostageState
{
    public override void Move()
    {
        base.Move();
    }
}