
using UnityEngine;

public class DistStateP1 : DistState
{
    private IADist _context;
    public DistStateP1(IADist ctx)
    {
        _context = ctx;
    }
    public override void Move()
    {
        for (int i = 0; i < _context.patrolWaypoint.Length;)
        {
            _context.agent.SetDestination(_context.patrolWaypoint[i].position);
            if (Vector3.Distance(_context.gameObject.transform.position,_context.patrolWaypoint[i].position) <= _context.distanceToChangeWaypoint)
            {
                i++;
            }
        }
        Ray ray = new Ray(_context.gameObject.transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,maxDistance))
        {
            if (hit.collider.tag == "Player")
            {
                Shoot();
            }
        }
    }

    public override void Shoot()
    {
        
    }
}
