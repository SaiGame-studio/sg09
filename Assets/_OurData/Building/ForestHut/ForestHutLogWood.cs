using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestHutLogWood : ResHolder 
{
    protected override void ResetValues()
    {
        base.ResetValues();
        this.resMax = 7;
    }
}
