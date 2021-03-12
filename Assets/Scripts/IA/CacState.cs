using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CacState 
{
   public virtual void Move(IACac ctx)
   {
      
   }

   public virtual void Attack(IACac ctx)
   {
      
   }

   public virtual void RunToPlayer(IACac ctx)
   {
      
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
      public override void Move(IACac ctx)
      {
         base.Move(ctx);
      }

      public override void Attack(IACac ctx)
      {
         base.Attack(ctx);d
      }
   }
   
}
