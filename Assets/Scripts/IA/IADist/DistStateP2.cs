﻿using UnityEngine;

public class DistStateP2 : DistState
{
    public DistStateP2(IADist ctx)
    {
        context = ctx;
        maxDistance = 30;
    }

    public override void Move()
    {
        context.agent.SetDestination(context.miradorTransform.position);
        context.transform.Rotate(0,Time.deltaTime*20,0);
        
        timerToShootAgain -= Time.deltaTime;
      //  Debug.Log(timerToShootAgain);
        Vector3 IAPos = context.gameObject.transform.position;
        Vector3 IAForward = context.gameObject.transform.forward * maxDistance;
        Vector3 IARight = context.gameObject.transform.right * 5;
        Ray ray1 = new Ray(IAPos, IAForward);
        Ray ray2 = new Ray(IAPos, IAForward - IARight);
        Ray ray3 = new Ray(IAPos, IAForward + IARight);
        Ray[] rays = new[]
        {
            ray1, ray2, ray3
        };
        RaycastHit hit;
        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Player") && timerToShootAgain <= 0)
                {
                    Shoot();
                }
            }
        }
    }
}