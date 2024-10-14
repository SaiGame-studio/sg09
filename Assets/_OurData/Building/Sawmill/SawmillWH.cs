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
        this.resources.Add(new Resource(ResourceName.logwood, 0));
        this.resources.Add(new Resource(ResourceName.blank, 0));
        Debug.LogWarning(transform.name + ": LoadResources", gameObject);
    }

    public override ResHolder ResNeed2Move()
    {
        ResHolder resHolder = this.GetRes(ResourceName.blank);
        if (resHolder.Current() > 0) return resHolder;
        return null;
    }

    public override List<Resource> NeedResoures()
    {
        List<Resource> resources = new();

        ResHolder logwood = this.GetRes(ResourceName.logwood);
        int number = logwood.resMax - logwood.resCurrent;
        Resource resLogwood = new(ResourceName.logwood, number);

        if (resLogwood.number > 0) resources.Add(resLogwood);

        return resources;
    }
}
