using System.Collections.Generic;
using UnityEngine;

public class SawmillWH : Warehouse
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadResources();
    }

    protected virtual void LoadResources()
    {
        if (this.resources.Count > 0) return;
        this.resources.Add(new Resource(ResourceName.logwood, 0, 4));
        this.resources.Add(new Resource(ResourceName.blank, 0, 8));
        Debug.LogWarning(transform.name + ": LoadResources", gameObject);
    }

    public override List<Resource> ResNeedToMove(WorkerCtrl worker, bool getNumber)
    {
        List<Resource> resources = new();
        Resource res = this.GetResource(ResourceName.blank);
        int number = res.NumberFinal();
        if (getNumber) number = res.Number;

        int carryCount = worker.inventory.CarryCount;
        if (number > carryCount) number = carryCount;
        if (number > 0) resources.Add(new Resource(res.CodeName, number));
        return resources;
    }

    public override List<Resource> NeedResoures()
    {
        List<Resource> resources = new();

        Resource logwood = this.GetResource(ResourceName.logwood);
        int number = logwood.Max - logwood.NumberFinal();
        Resource resLogwood = new(ResourceName.logwood, number);

        if (resLogwood.NumberFinal() > 0) resources.Add(resLogwood);

        return resources;
    }
}
