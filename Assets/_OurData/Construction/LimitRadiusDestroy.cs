using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LimitRadiusDestroy : LimitRadius
{
    //[Header("Radius Destroy")]

    protected override void ResetValues()
    {
        base.ResetValues();
        this.buildRadius = 1;
    }

    public override bool IsCollided()
    {
        if (collideObjects.Count < 1) return false;

        List<int> layers = new List<int>
        {
            MyLayerManager.instance.layerTree,
            MyLayerManager.instance.layerBuilding,
        };
        this.CleanByLayers(layers);

        return false;
    }
}
