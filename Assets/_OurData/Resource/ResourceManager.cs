using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SaiSingleton<ResourceManager>
{
    [SerializeField] protected List<Resource> resources = new();
    [SerializeField] protected List<WarehouseCtrl> warehouses = new();
    
    [SerializeField] protected int generatorIndex = 0;
    [SerializeField] protected int chunkCount = 70;
    [SerializeField] protected List<ResGenerator> generators;

    protected virtual void FixedUpdate()
    {
        //this.AllResourceUpdate();
        this.Generating();
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

    //TODO: remote this method after 6 months, 11.2024
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

    public virtual void AddGenerator(ResGenerator resGenerator)
    {
        if (this.generators.Contains(resGenerator)) return;
        this.generators.Add(resGenerator);
    }

    protected virtual void Generating()
    {
        if (this.generators.Count <= 0) return;
        for (int i = 0; i < this.chunkCount; i++)
        {
            ResGenerator generator = this.generators[this.generatorIndex];
            generator.Generating();
            this.generatorIndex++;
            if (this.generatorIndex >= this.generators.Count) this.generatorIndex = 0;
        }
    }
}
