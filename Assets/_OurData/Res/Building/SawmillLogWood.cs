using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawmillLogWood: ResHolder 
{
    protected override void ResetValues()
    {
        base.ResetValues();
        this.resMax = 4;
    }
}
