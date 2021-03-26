using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatrollerState
{
    protected int _index;
    
    public virtual void Move(IAPatroller ctx)
    {
        if (!ctx.fleeing)
        {
            ctx.agent.SetDestination(ctx.patrolWaypoint[_index].position);
            
            if (Vector3.Distance(ctx.gameObject.transform.position, ctx.patrolWaypoint[_index].position) <=
                ctx.distanceToChangeWaypoint)
            {
                _index++;
            }

            if (_index >= ctx.patrolWaypoint.Length)
            {
                _index = 0;
            }
        }
        else
        {
            Vector3 dirToPlayer = (ctx.transform.position - ctx.player.position).normalized;
            Vector3 newPosTarget = ctx.transform.position + (dirToPlayer * 25);

            ctx.agent.SetDestination(newPosTarget);
        }
        
    }
    public virtual void PlaceMine(IAPatroller ctx)
    {
        
    }
}

public class PatrollerStateP1 : PatrollerState
{
}

public class PatrollerStateP2 : PatrollerState
{
    public override void PlaceMine(IAPatroller ctx)
    {
        base.PlaceMine(ctx);
        if (!ctx.minePlaced)
        {
            ctx.minePlaced = true;
            
        }
    }
}