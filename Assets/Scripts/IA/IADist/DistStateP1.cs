using UnityEngine;

public class DistStateP1 : DistState
{
    public DistStateP1(IADist ctx)
    {
        context = ctx;
        maxDistanceMultiplier = 1;
    }

    public override void Move()
    {
        Debug.Log("In");
        context.agent.SetDestination(context.patrolWaypoint[index].position);
        if (Vector3.Distance(context.gameObject.transform.position, context.patrolWaypoint[index].position) <=
            context.distanceToChangeWaypoint)
        {
            index++;
        }

        if (index >= context.patrolWaypoint.Length)
        {
            index = 0;
        }

        if (DetectPlayer(context) && timerToShootAgain <= 0)
        {
            Shoot();
        }
    }
    
    public bool DetectPlayer(IADist ctx)
    {
        Debug.Log("In here");
        
        float anglePerRay = ctx.angle / ctx.rayCount;
        
        for (int i = 0; i < ctx.rayCount; i++)
        {
            Vector3 rayForward = new Vector3(ctx.detectionRange * maxDistanceMultiplier * Mathf.Sin((Mathf.Deg2Rad * anglePerRay * i) - (ctx.angle/2) * Mathf.Deg2Rad + ctx.transform.localEulerAngles.y * Mathf.Deg2Rad), 
                0, 
                ctx.detectionRange * maxDistanceMultiplier * Mathf.Cos((Mathf.Deg2Rad * anglePerRay * i) - (ctx.angle/2) * Mathf.Deg2Rad + ctx.transform.localEulerAngles.y * Mathf.Deg2Rad));
            
            Debug.Log("is this normal");
            Debug.DrawRay(ctx.transform.position, rayForward);
            
            Ray ray = new Ray(ctx.transform.position, rayForward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, ctx.detectionRange))
            {
                if (hit.collider.tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }
}