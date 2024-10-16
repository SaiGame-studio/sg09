using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : ResGenerator
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadResCreate();
        this.SetLimit();
    }

    protected virtual void LoadResCreate()
    {
        Resource res = new(ResourceName.water, 1);
        this.resCreate.Clear();
        this.resCreate.Add(res);
    }

    protected virtual void SetLimit()
    {
        Resource water = this.GetResource(ResourceName.water);
        water.SetMax(7);
    }

    public override List<Resource> ResNeed2Move()
    {
        List<Resource> resources = new();
        Resource res = this.GetResource(ResourceName.water);
        if (res.Number > 0) resources.Add(res);
        return resources;
    }
}
