using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CacState
{
   protected int _index;
  
   public virtual void Move(IACac ctx)
   {
      ctx.isAttack = false;
      // if player detected ia go toward the player
      if (!ctx.isPlayerDetected)
      {
         if (ctx.isRushing)
         {
            ctx.agent.SetDestination(ctx.rushPos);
            if (Vector3.Distance(ctx.rushPos, ctx.transform.position) <= 2)
            {
               ctx.isRushing = false;
            }
         }
         else
         {
            ctx.agent.SetDestination(ctx.patrolWaypoint[_index].position);
            if (Vector3.Distance(ctx.gameObject.transform.position, ctx.patrolWaypoint[_index].position) <=
                ctx.distanceToChangeWaypoint)
            {
               _index++;
            }

            if (_index >= ctx.patrolWaypoint.Length)
            {
               _index = 0;
            }
         }
         
         
        
      }
    
   }

   public virtual void Attack(IACac ctx)
   {
      //If player detected the Ia go to its curretn pos and if is not already attacked the ia attack
      if (ctx.isPlayerDetected)
      {
         ctx.agent.SetDestination(ctx.transform.position);
       
   
         if (!ctx.isAlreadyAttacked)
         {
            //Logic attack code
            if (ctx.obj_spoted.GetComponent<IAHostage>())
            {
               ctx.obj_spoted.GetComponent<IAHostage>().TakeDamage(ctx.baseDamage + ctx.damageBoost);
            }
            else
            {
               ctx.obj_spoted.GetComponent<PlayerLife>().TakeDamage(ctx.baseDamage + ctx.damageBoost);
            }
            
            ctx.isAttack = true;
            ctx.isAlreadyAttacked = true;
            ctx.Invoke(nameof(ctx.ResetAttack), ctx.timeBetweenAttack);
         }
      }
   }

   public virtual void RunToPlayer(IACac ctx)
   {
      //If player detected the Ia go to the player with a calculate distance  and attack if he is in a range
      if (ctx.isPlayerDetected)
      {
         
         ctx.agent.speed += ctx.speedBoost;
         ctx.agent.speed = Mathf.Clamp(ctx.agent.speed, 0, 5.5f);
         ctx.agent.SetDestination(ctx.obj_spoted.transform.position);
         if (Vector3.Distance(ctx.agent.transform.position, ctx.obj_spoted.transform.position) <= ctx.agent.stoppingDistance)
         {
           
            ctx.currentState.Attack(ctx);
         }
         

      }
   }

}

public class CacStateP1: CacState
{
   public override void Move(IACac ctx)
   {
      base.Move(ctx);
   }

   public override void Attack(IACac ctx)
   {
      base.Attack(ctx);
   }
}
   
public class CacStateP2: CacState
{
   public CacStateP2(IACac ctx)
   {
      ctx.damageBoost = 5;
      ctx.speedBoost = 2;
      
   }



   public override void Move(IACac ctx)
   {
      base.Move(ctx);
   }

   public override void Attack(IACac ctx)
   {
      base.Attack(ctx);
   }
}
