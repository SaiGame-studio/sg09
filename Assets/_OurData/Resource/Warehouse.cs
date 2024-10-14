using System.Collections.Generic;
using UnityEngine;

public class Warehouse : SaiBehaviour
{
    [Header("Warehouse")]
    [SerializeField] protected bool isFull = false;
    [SerializeField] protected List<ResHolder> resHolders;
    [SerializeField] protected List<Resource> resources;

    protected virtual void FixedUpdate()
    {
        this.isFull = this.IsFull();
    }

    protected override void LoadComponents()
    {
        this.LoadHolders();
    }

    protected virtual void LoadHolders()
    {
        if (this.resHolders.Count > 0) return;

        Transform res = transform.Find("Res");
        foreach (Transform resTran in res)
        {
            Debug.Log(resTran.name);
            ResHolder resHolder = resTran.GetComponent<ResHolder>();
            if (resHolder == null) continue;
            this.resHolders.Add(resHolder);
        }

        Debug.Log(transform.name + ": LoadHolders", gameObject);
    }

    public virtual ResHolder GetRes(ResourceName name)
    {
        return this.resHolders.Find((holder) => holder.Name() == name);
    }

    public virtual List<ResHolder> GetStockedResources()
    {
        return this.resHolders.FindAll((holder) => holder.resCurrent > 0);
    }

    public virtual void AddByList(List<Resource> addResources)
    {
        foreach (Resource addResource in addResources)
        {
            this.AddResource(addResource.codeName, addResource.number);
        }
    }

    public virtual bool AddResource(ResourceName resourceName, int number)
    {
        Resource resource = this.GetResource(resourceName);
        if (!resource.TryToAdd(number)) return false;

        resource.Add(number);

        ResHolder res = this.GetRes(resourceName);
        res.Add(number);

        return true;
    }

    public virtual bool RemoveResource(ResourceName resourceName, int number)
    {
        Resource resource = this.GetResource(resourceName);
        if (!resource.TryToRemove(number)) return false;

        resource.Remove(number);

        ResHolder res = this.GetRes(resourceName);
        res.Deduct(number);

        return true;
    }

    public virtual Resource GetResource(ResourceName resourceName)
    {
        Resource resource = this.resources.Find((resource) => resource.codeName == resourceName);
        if (resource == null)
        {
            resource = new Resource(resourceName, 0);
            this.resources.Add(resource);
        }
        return resource;
    }

    public virtual bool IsFull()
    {
        foreach (ResHolder resHolder in this.resHolders)
        {
            if (!resHolder.IsMax()) return false;
        }
        return true;
    }

    public virtual ResHolder ResNeed2Move()
    {
        return null;
    }

    public virtual List<Resource> NeedResoures()
    {
        return new List<Resource>();//Do not return null
    }
}
