using UnityEngine;

public abstract class DistState
{
    public float maxDistance;
    
    protected int index;
    
    protected IADist _context;
    
    protected float _timerToShootAgain = 2f;
    public virtual void Move()
    {
        
    }
    public virtual void Shoot()
    {
        _timerToShootAgain = 2.5f;
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
                if (hit.collider.CompareTag("Player"))
                {
                    _context.transform.LookAt(hit.collider.gameObject.transform);
                    ObjectPooler.instance.SpawnFromPool("Bullet", _context.transform.position,_context.transform.rotation);
                    return ;
                }
            }
        }
    }
}
