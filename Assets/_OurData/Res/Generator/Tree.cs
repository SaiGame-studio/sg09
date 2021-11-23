using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ResGenerator
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadResCreate();
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
}
