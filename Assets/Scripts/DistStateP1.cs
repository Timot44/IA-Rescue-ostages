﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DistStateP1 : DistState
{
    private IADist context;
    public DistStateP1(IADist ctx)
    {
        context = ctx;
    }
    public override void Move()
    {
        foreach (var patrolPoint in context.patrolPoints)
        {
            
        }
    }

    public override void Shoot()
    {
        
    }
}
