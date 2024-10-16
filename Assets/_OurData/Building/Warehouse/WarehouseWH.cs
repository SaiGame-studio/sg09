using UnityEngine;

public class WarehouseWH : Warehouse
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadResources();
    }

    protected virtual void LoadResources()
    {
        if (this.resources.Count > 0) return;
        this.resources.Add(new Resource(ResourceName.water, 0, 9));
        Debug.LogWarning(transform.name + ": LoadResources", gameObject);
    }
}
