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

    public override List<Resource> ResNeedToMove(WorkerCtrl worker, bool getNumber)
    {
        List<Resource> resources = new();
        Resource res = this.GetResource(ResourceName.water);
        int number = res.NumberFinal();
        if (getNumber) number = res.Number;

        int carryCount = worker.inventory.CarryCount;
        if (number > carryCount) number = carryCount;
        if (number > 0) resources.Add(new Resource(res.CodeName, number));
        return resources;
    }
}
