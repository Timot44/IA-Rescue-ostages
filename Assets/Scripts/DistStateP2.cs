using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistStateP2 : DistState
{
    private IADist _context;
    public DistStateP2(IADist ctx)
    {
        _context = ctx;
    }
    
    public override void Shoot()
    {

    }
}
