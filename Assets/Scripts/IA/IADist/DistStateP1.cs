using UnityEngine;

public class DistStateP1 : DistState
{
    public DistStateP1(IADist ctx)
    {
        _context = ctx;
        maxDistance = 5;
    }

    public override void Move()
    {
        _context.agent.SetDestination(_context.patrolWaypoint[index].position);
        if (Vector3.Distance(_context.gameObject.transform.position, _context.patrolWaypoint[index].position) <=
            _context.distanceToChangeWaypoint)
        {
            index++;
        }

        if (index >= _context.patrolWaypoint.Length)
        {
            index = 0;
        }

        Vector3 IAPos = _context.gameObject.transform.position;
        Vector3 IAForward = _context.gameObject.transform.forward * maxDistance;
        Vector3 IARight = _context.gameObject.transform.right * 2;
        Ray ray1 = new Ray(IAPos, IAForward);
        Ray ray2 = new Ray(IAPos, IAForward - IARight);
        Ray ray3 = new Ray(IAPos, IAForward + IARight);
        Ray[] rays = new[]
        {
            ray1, ray2, ray3
        };
        _timerToShootAgain -= Time.deltaTime;
        if (_timerToShootAgain <= 2)
        {
            _context.agent.isStopped = false;
        }

        RaycastHit hit;
        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.tag == "Player" && _timerToShootAgain <= 0)
                {
                    _context.agent.isStopped = true;
                    Shoot();
                }
            }
        }
    }
}