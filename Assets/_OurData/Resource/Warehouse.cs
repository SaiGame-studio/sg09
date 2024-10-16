using System.Collections.Generic;
using UnityEngine;

public class Warehouse : SaiBehaviour
{
    [Header("Warehouse")]
    [SerializeField] protected bool isFull = false;
    [SerializeField] protected List<Resource> resources;

    protected virtual void FixedUpdate()
    {
        this.isFull = this.IsFull();
    }

    public virtual void AddByList(List<Resource> addResources, bool isGenerate)
    {
        foreach (Resource addResource in addResources)
        {
            this.AddResource(addResource.CodeName, addResource.Number, isGenerate);
        }
    }

    public virtual bool AddResource(ResourceName resourceName, int number, bool isGenerate)
    {
        Resource resource = this.GetResource(resourceName);
        if (!resource.TryToAdd(number)) return false;
        if(isGenerate) resource.Generate(number);
        else resource.Add(number);
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

    public virtual void Deducting(List<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            this.Deducting(resource.CodeName, resource.Number);
        }
    }

    public virtual void Deducting(ResourceName resourceName, int number)
    {
        Resource resInWarehouse = this.GetResource(resourceName);
        resInWarehouse.Deducting(number);
    }

    public virtual void Adding(List<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            this.Adding(resource.CodeName, resource.Number);
        }
    }

    public virtual void Adding(ResourceName resourceName, int number)
    {
        Resource resInWarehouse = this.GetResource(resourceName);
        resInWarehouse.Adding(number);
    }
}
