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

    public override Resource ResNeed2Move()
    {
        Resource logwood = this.GetResource(ResourceName.logwood);
        if (logwood.Number > 0) return logwood;
        return null;
    }
}
