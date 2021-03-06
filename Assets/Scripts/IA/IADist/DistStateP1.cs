using UnityEngine;

public class DistStateP1 : DistState
{
    public DistStateP1(IADist ctx)
    {
        context = ctx;
        maxDistanceMultiplier = 1;
    }
//Move following a pattern
    public override void Move()
    {

        if (context.isRushing)
        {
            context.agent.SetDestination(context.rushPos);
            if (Vector3.Distance(context.rushPos, context.transform.position) <= 2)
            {
                context.isRushing = false;
            }
        }
        else
        {
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

            timerToShootAgain -= Time.deltaTime;

            if (DetectPlayer(context) && timerToShootAgain <= 0)
            {
                Shoot();
            }
        }
       
    }
//allow the detection of the player
    public bool DetectPlayer(IADist ctx)
    {
        

        float anglePerRay = ctx.angle / ctx.rayCount;

        for (int i = 0; i < ctx.rayCount; i++)
        {
            Vector3 rayForward = new Vector3(
                ctx.detectionRange * maxDistanceMultiplier * Mathf.Sin((Mathf.Deg2Rad * anglePerRay * i) -
                    (ctx.angle / 2) * Mathf.Deg2Rad + ctx.transform.localEulerAngles.y * Mathf.Deg2Rad),
                0,
                ctx.detectionRange * maxDistanceMultiplier * Mathf.Cos((Mathf.Deg2Rad * anglePerRay * i) -
                    (ctx.angle / 2) * Mathf.Deg2Rad + ctx.transform.localEulerAngles.y * Mathf.Deg2Rad));

            Debug.DrawRay(ctx.transform.position, rayForward);

            Ray ray = new Ray(ctx.transform.position, rayForward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, ctx.detectionRange))
            {
                if (hit.collider.tag == "Player")
                {
                    context.transform.LookAt(hit.collider.gameObject.transform);
                    return true;
                }
            }
        }

        return false;
    }
}