using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatrollerState
{
    protected int _index;
    
    public virtual void Move(IAPatroller ctx)
    {
        // Handle the movement for the patroller
        if (!ctx.fleeing)
        {
            // If its not fleeing the player, this patroller patrol on there path
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
            // If the patroller is fleeing, that part set a destination to the opposite side of the player position
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
    // The only behavior that change in state two is the fact that it place mine.
    public override void PlaceMine(IAPatroller ctx)
    {
        base.PlaceMine(ctx);
        // Check if a mine is already placed, if not, place a mine
        if (!ctx.minePlaced)
        {
            ctx.PlaceMine();
        }
    }
}