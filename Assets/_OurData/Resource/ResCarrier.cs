using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResCarrier : MonoBehaviour
{
    [SerializeField] protected int carryCount = 1;
    public int CarryCount => carryCount;
    [SerializeField] protected List<Resource> resources;

    public virtual Resource AddResource(ResourceName resourceName, int number)
    {
        Resource res = this.GetResByName(resourceName);
        res.Add(number);
        return res;
    }

    public virtual void AddByList(List<Resource> addResources)
    {
        foreach (Resource addResource in addResources)
        {
            this.AddResource(addResource.CodeName, addResource.Number);
        }
    }

    public virtual List<Resource> TakeAll()
    {
        List<Resource> resources = new(this.resources); //Clone
        this.resources = new List<Resource>();
        return resources;
    }

    public virtual Resource TakeFirst()
    {
        Resource res = this.resources[0];
        this.resources.RemoveAt(0);
        return res;
    }

    public virtual Resource GetResByName(ResourceName resourceName)
    {
        Resource res = this.resources.Find((x) => x.CodeName == resourceName);

        if (res == null)
        {
            res = new Resource(resourceName, 0);
            this.resources.Add(res);
        }

        return res;
    }

    public virtual List<Resource> Resources()
    {
        return this.resources;
    }
}
