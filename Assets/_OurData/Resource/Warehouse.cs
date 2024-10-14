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

    public virtual void AddByList(List<Resource> addResources)
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
        if (!resource.TryToRemove(number)) return false;
        resource.Remove(number);
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

    public virtual Resource ResNeed2Move()
    {
        return null;
    }

    public virtual List<Resource> NeedResoures()
    {
        return new List<Resource>();//Do not return null
    }
}
