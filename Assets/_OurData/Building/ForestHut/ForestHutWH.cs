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

    public override List<Resource> ResNeed2Move()
    {
        List<Resource> resources = new();
        Resource res = this.GetResource(ResourceName.logwood);
        if (res.Number > 0) resources.Add(res);
        return resources;
    }
}
