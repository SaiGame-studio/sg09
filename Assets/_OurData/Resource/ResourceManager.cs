using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SaiSingleton<ResourceManager>
{
    [SerializeField] protected List<Resource> resources = new();
    [SerializeField] protected List<WarehouseCtrl> warehouses = new();

    protected virtual void FixedUpdate()
    {
        this.AllResourceUpdate();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAllResources();
    }

    protected virtual void LoadAllResources()
    {
        if (this.resources.Count > 0) return;
        Resource resource;
        foreach (ResourceName resName in Enum.GetValues(typeof(ResourceName)))
        {
            if (resName == ResourceName.noResource) continue;
            resource = new Resource(resName);
            this.resources.Add(resource);
        }
        Debug.LogWarning(transform.name + ": LoadAllResources", gameObject);
    }

    public virtual Resource GetResource(ResourceName resName)
    {
        return this.resources.Find(resource => resource.CodeName == resName);
    }

    protected virtual void AllResourceUpdate()
    {
        this.warehouses = BuildingManager.Instance.Warehouses();
        Resource resInWareHouse;
        foreach (Resource resource in this.resources)
        {
            resource.SetNumber(0);
            foreach (WarehouseCtrl warehouseCtrl in this.warehouses)
            {
                resInWareHouse = warehouseCtrl.warehouse.GetResource(resource.CodeName);
                resource.Add(resInWareHouse.Number);
            }
        }
    }
}
