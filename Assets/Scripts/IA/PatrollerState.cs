using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatrollerState
{
    public virtual void Move(IAPatroller ctx)
    {
        
    }
    public virtual void PlaceMine(IAPatroller ctx)
    {
        
    }
}

public class PatrollerStateP1 : PatrollerState
{
    public override void Move(IAPatroller ctx)
    {
        base.Move(ctx);
    }
}

public class PatrollerStateP2 : PatrollerState
{
    public override void Move(IAPatroller ctx)
    {
        base.Move(ctx);
    }

    public override void PlaceMine(IAPatroller ctx)
    {
        base.PlaceMine(ctx);
    }
}