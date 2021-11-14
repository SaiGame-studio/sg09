using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    [SerializeField] protected List<Resource> resources;

    protected virtual void Awake()
    {
        if (ResourceManager.instance != null) Debug.LogError("Only 1 ResourceManager allow");
        ResourceManager.instance = this;
    }

    public virtual Resource AddByName(ResourceName resourceName, int number)
    {
        Resource resource = this.GetByName(resourceName);
        resource.number += number;
        return resource;
    }

    public virtual Resource GetByName(ResourceName resourceName)
    {
        Resource resource = this.resources.Find((x) => x.name == resourceName);
        if(resource == null)
        {
            resource = new Resource
            {
                name = resourceName,
                number = 0
            };
            this.resources.Add(resource);
        }

        return resource;
    }
}
