using System.Collections;
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
        Resource res = new Resource
        {
            name = ResourceName.water,
            number = 1
        };

        this.resCreate.Clear();
        this.resCreate.Add(res);
    }

    protected virtual void SetLimit()
    {
        ResHolder resHolder = this.GetResource(ResourceName.water);
        resHolder.SetLimit(7);
    }
}
