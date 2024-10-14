using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    [SerializeField] protected List<Resource> resources;

    protected void Awake()
    {
        //if (ResourceManager.instance != null) Debug.LogError("On 1 ResourceManager allow");
        //ResourceManager.instance = this;
    }

    public virtual Resource AddResource(ResourceName resourceName, int number)
    {
        Resource res = this.GetResByName(resourceName);
        res.number += number;
        return res;
    }

    public virtual Resource GetResByName(ResourceName resourceName)
    {
        Resource res = this.resources.Find((x) => x.codeName == resourceName);

        if (res == null)
        {
            res = new Resource(resourceName, 0);
            this.resources.Add(res);
        }

        return res;
    }
}
