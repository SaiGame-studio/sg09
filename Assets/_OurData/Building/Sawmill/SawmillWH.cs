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

    public override List<Resource> ResNeed2Move()
    {
        List<Resource> resources = new();
        Resource blank = this.GetResource(ResourceName.blank);
        if (blank.Number > 0) resources.Add(blank);
        return resources;
    }

    public override List<Resource> NeedResoures()
    {
        List<Resource> resources = new();

        Resource logwood = this.GetResource(ResourceName.logwood);
        int number = logwood.Max - logwood.Number;
        Resource resLogwood = new(ResourceName.logwood, number);

        if (resLogwood.Number > 0) resources.Add(resLogwood);

        return resources;
    }
}
