using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResCarrier : MonoBehaviour
{
    public int carryCount = 1;
    [SerializeField] protected List<Resource> resources;

    public virtual Resource AddResource(ResourceName resourceName, float number)
    {
        Resource res = this.GetResByName(resourceName);
        res.number += number;
        return res;
    }

    public virtual void AddByList(List<Resource> addResources)
    {
        foreach(Resource addResource in addResources)
        {
            this.AddResource(addResource.name, addResource.number);
        }
    }

    public virtual List<Resource> TakeAll()
    {
        List<Resource> resources = new List<Resource>(this.resources); //Clone
        this.resources = new List<Resource>();
        return resources;
    }

    public virtual Resource TakeFirst()
    {
        Resource res = this.resources[0];
        this.resources.RemoveAt(0);
        return res;
    }

    public virtual Resource GetFirst()
    {
        return this.resources[0];
    }

    public virtual Resource GetResByName(ResourceName resourceName)
    {
        Resource res = this.resources.Find((x) => x.name == resourceName);

        if (res == null)
        {
            res = new Resource
            {
                name = resourceName
            };

            this.resources.Add(res);
        }

        return res;
    }

    public virtual List<Resource> Resources()
    {
        return this.resources;
    }
}
