using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LimitRadiusSmall: LimitRadius
{
    //[Header("Radius Small")]

    protected override void ResetValues()
    {
        base.ResetValues();
        this.buildRadius = 1;
    }
}
