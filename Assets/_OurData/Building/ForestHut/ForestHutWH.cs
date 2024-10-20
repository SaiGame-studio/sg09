using System.Collections.Generic;
using UnityEngine;

public class ForestHutWH : Warehouse
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadResources();
    }

    protected virtual void LoadResources()
    {
        if (this.resources.Count > 0) return;
        this.resources.Add(new Resource(ResourceName.logwood, 0, 7));
        Debug.LogWarning(transform.name + ": LoadResources", gameObject);
    }

    public override List<Resource> ResNeedToMove(WorkerCtrl worker, bool getNumber)
    {
        List<Resource> resources = new();
        Resource res = this.GetResource(ResourceName.logwood);
        int number = res.NumberFinal();
        if (getNumber) number = res.Number;

        int carryCount = worker.inventory.CarryCount;
        if (number > carryCount) number = carryCount;
        if (number > 0) resources.Add(new Resource(res.CodeName, number));
        return resources;
    }
}
