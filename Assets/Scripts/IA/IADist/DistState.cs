using UnityEngine;

public abstract class DistState
{
    public float maxDistanceMultiplier;
    
    protected int index;
    
    protected IADist context;
    
    protected float timerToShootAgain = 2f;
    
    public virtual void Move()
    {
        
    }
    protected void Shoot()
    {
        timerToShootAgain = 2.5f;
        ObjectPooler.instance.SpawnFromPool("Bullet", context.transform.position,context.transform.rotation);
    }
}
