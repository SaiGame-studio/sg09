using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogwoodGenerator : ResGenerator
{
    //[Header("LogwoodGenerator")]

    protected override void ResetValues()
    {
        base.ResetValues();
        this.createDelay = 60f;
    }

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
            name = ResourceName.logwood,
            number = 1
        };

        this.resCreate.Clear();
        this.resCreate.Add(res);
    }

    protected virtual void SetLimit()
    {
        ResHolder resHolder = this.GetResource(ResourceName.logwood);
        resHolder.SetLimit(1);
    }

    protected override void Creating()
    {
        if (this.IsAllResMax()) this.canCreate = false;
        base.Creating();
    }
}
