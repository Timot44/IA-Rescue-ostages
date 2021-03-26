using UnityEngine;

public class DistStateP1 : DistState
{
    public DistStateP1(IADist ctx)
    {
        context = ctx;
        maxDistance = 15;
    }

    public override void Move()
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

        Vector3 IAPos = context.gameObject.transform.position;
        Vector3 IAForward = context.gameObject.transform.forward * maxDistance;
        Vector3 IARight = context.gameObject.transform.right * 4;
        Ray ray1 = new Ray(IAPos, IAForward);
        Ray ray2 = new Ray(IAPos, IAForward - IARight);
        Ray ray3 = new Ray(IAPos, IAForward + IARight);
        Ray[] rays = new[]
        {
            ray1, ray2, ray3
        };
        timerToShootAgain -= Time.deltaTime;
        if (timerToShootAgain <= 2)
        {
            context.agent.isStopped = false;
        }

        RaycastHit hit;
        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.tag == "Player" && timerToShootAgain <= 0)
                {
                    context.agent.isStopped = true;
                    Shoot();
                }
            }
        }
    }
}