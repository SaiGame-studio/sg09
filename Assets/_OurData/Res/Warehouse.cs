using System.Collections.Generic;
using UnityEngine;

public class Warehouse : SaiBehaviour
{
    [Header("Warehouse")]
    //public BuildingType buildingType = BuildingType.workStation;
    [SerializeField] protected bool isFull = false;
    [SerializeField] protected List<ResHolder> resHolders;

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

    public virtual ResHolder GetResource(ResourceName name)
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
            this.AddResource(addResource.name, addResource.number);
        }
    }

    public virtual ResHolder AddResource(ResourceName resourceName, float number)
    {
        ResHolder res = this.GetResource(resourceName);
        res.Add(number);
        return res;
    }

    public virtual ResHolder RemoveResource(ResourceName resourceName, float number)
    {
        ResHolder res = this.GetResource(resourceName);
        if (res.Current() < number) return null;
        res.Deduct(number);
        return res;
    }

    public virtual bool IsFull()
    {
        foreach (ResHolder resHolder in this.resHolders)
        {
            if (!resHolder.IsMax()) return false;
        }

        //Debug.Log("Warehouse IsFull", gameObject);
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
