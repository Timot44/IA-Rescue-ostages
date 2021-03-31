using UnityEngine;

public class DistStateP2 : DistState
{
    public DistStateP2(IADist ctx)
    {
        context = ctx;
        maxDistanceMultiplier = 2;
    }

    public override void Move()
    {
        context.agent.SetDestination(context.miradorTransform.position);
        context.transform.Rotate(0, Time.deltaTime * 20, 0);
        timerToShootAgain -= Time.deltaTime;
        if (DetectPlayer(context) && timerToShootAgain <= 0)
        {
            Shoot();
        }
    }

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

            Debug.Log("is this normal");
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