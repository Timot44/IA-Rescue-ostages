using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CacState
{
   protected int _index;
  
   public virtual void Move(IACac ctx)
   {
      if (!ctx.isPlayerDetected)
      {
         ctx.agent.SetDestination(ctx.patrolWaypoint[_index].position);
         ctx.isAttack = false;
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
      else
      {
         Vector3 dirToPlayer = (ctx.transform.position - ctx.player.position).normalized;
         Vector3 newPosTarget = ctx.transform.position + (dirToPlayer * 25);

         ctx.agent.SetDestination(newPosTarget);
      }
   }

   public virtual void Attack(IACac ctx)
   {
      if (ctx.isPlayerDetected)
      {
         ctx.agent.SetDestination(ctx.transform.position);
         

         if (!ctx.isAlreadyAttacked)
         {
            //Logic attack code
            ctx.player.GetComponent<PlayerLife>().TakeDamage(ctx.baseDamage + ctx.damageBoost);
            ctx.isAttack = true;
            ctx.isAlreadyAttacked = true;
            ctx.Invoke(nameof(ctx.ResetAttack), ctx.timeBetweenAttack);
         }
      }
   }

   public virtual void RunToPlayer(IACac ctx)
   {
      if (ctx.isPlayerDetected)
      {
         ctx.isAttack = false;
         ctx.agent.speed = ctx.agent.speed + ctx.speedBoost;
         ctx.agent.SetDestination(ctx.player.position);
         if (Vector3.Distance(ctx.agent.transform.position, ctx.player.position) <= ctx.agent.stoppingDistance)
         {
            Debug.Log("IA CAC ATTACK");
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
