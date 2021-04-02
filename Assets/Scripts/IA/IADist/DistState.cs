using UnityEngine;

public abstract class DistState
{
    public float maxDistanceMultiplier;
    
    protected int index;
    
    protected IADist context;
    
    protected float timerToShootAgain = 2f;
    //this void is override depending on which phase the AI is in
    public virtual void Move()
    {
        
    }
    //the AI shoot in the same way in both phases
    protected void Shoot()
    {
        timerToShootAgain = 2.5f;
        ObjectPooler.instance.SpawnFromPool("Bullet", context.transform.position,context.transform.rotation);
    }
}
