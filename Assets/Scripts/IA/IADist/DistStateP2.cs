using UnityEngine;

public class DistStateP2 : DistState
{
    public DistStateP2(IADist ctx)
    {
        _context = ctx;
        maxDistance = 30;
    }

    public override void Move()
    {        
        _timerToShootAgain -= Time.deltaTime;
        Vector3 IAPos = _context.gameObject.transform.position;
        Vector3 IAForward = _context.gameObject.transform.forward * maxDistance;
        Vector3 IARight = _context.gameObject.transform.right * 4;
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
                if (hit.collider.tag == "Player" && _timerToShootAgain <=0)
                {
                    Shoot();
                }
            }
        }
    }
}
