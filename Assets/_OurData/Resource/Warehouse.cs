using System.Collections.Generic;
using UnityEngine;

public class Warehouse : SaiBehaviour
{
    [Header("Inventory")]
    [SerializeField] protected bool isFull = false;
    [SerializeField] protected List<Resource> resources;
    public List<Resource> Resources => resources;

    //protected virtual void FixedUpdate()
    //{
    //    this.isFull = this.IsFull();
    //}

    public virtual void AddResources(List<Resource> addResources)
    {
        foreach (Resource addResource in addResources)
        {
            this.AddResource(addResource.CodeName, addResource.Number);
        }
    }

    public virtual bool AddResource(ResourceName resourceName, int number)
    {
        Resource resource = this.GetResource(resourceName);
        if (!resource.TryToAdd(number)) return false;
        resource.Add(number);
        return true;
    }

    public virtual bool RemoveResource(ResourceName resourceName, int number)
    {
        Resource resource = this.GetResource(resourceName);
        if (!resource.TryToDeduct(number)) return false;
        resource.Deduct(number);
        return true;
    }

    public virtual Resource GetResource(ResourceName resourceName)
    {
        Resource resource = this.resources.Find((resource) => resource.CodeName == resourceName);
        if (resource == null)
        {
            resource = new Resource(resourceName, 0);
            this.resources.Add(resource);
        }
        return resource;
    }

    public virtual bool IsFull()
    {
        if (this.resources.Count == 0) return false;
        foreach (Resource resource in this.resources)
        {
            if (!resource.IsMax()) return false;
        }
        return true;
    }


    public virtual List<Resource> ResNeedToMove(WorkerCtrl worker, bool getNumber)
    {
        return new List<Resource>();//Do not return null
    }

    public virtual List<Resource> NeedResoures()
    {
        return new List<Resource>();//Do not return null
    }

    public virtual void WillDeduct(List<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            this.WillDeduct(resource.CodeName, resource.Number);
        }
    }

    public virtual void WillDeduct(ResourceName resourceName, int number)
    {
        Resource resInWarehouse = this.GetResource(resourceName);
        resInWarehouse.WillDeduct(number);
    }

    public virtual void Deducted(ResourceName resourceName, int number)
    {
        Resource resInWarehouse = this.GetResource(resourceName);
        resInWarehouse.Deducted(number);
    }

    public virtual void WillAdd(List<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            this.WillAdd(resource.CodeName, resource.Number);
        }
    }

    public virtual void WillAdd(ResourceName resourceName, int number)
    {
        Resource resInWarehouse = this.GetResource(resourceName);
        resInWarehouse.WillAdd(number);
    }

    public virtual void Added(List<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            this.Added(resource.CodeName, resource.Number);
        }
    }

    public virtual void Added(ResourceName resourceName, int number)
    {
        Resource resInWarehouse = this.GetResource(resourceName);
        resInWarehouse.Added(number);
    }
}
