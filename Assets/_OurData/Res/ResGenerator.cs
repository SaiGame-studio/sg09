using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResGenerator : Warehouse
{
    [Header("ResGenerator")]
    [SerializeField] protected List<Resource> resCreate;
    [SerializeField] protected List<Resource> resRequire;
    public bool canCreate = true;
    [SerializeField] protected float createTimer = 0f;
    [SerializeField] protected float createDelay = 7f;

    protected override void FixedUpdate()
    {
        this.Creating();
    }

    protected virtual void Creating()
    {
        if (!this.canCreate) return;

        this.createTimer += Time.fixedDeltaTime;
        if (this.createTimer < this.createDelay) return;
        this.createTimer = 0;

        if (!this.IsRequireEnough()) return;
        
        foreach(Resource res in this.resCreate)
        {
            ResHolder resHolder = this.GetHolder(res.name);
            resHolder.Add(res.number);
        }
    }

    protected virtual bool IsRequireEnough()
    {
        if (this.resRequire.Count < 1) return true;

        //TODO: this is not done yet
        return false;
    }

    public virtual float GetCreateDelay()
    {
        return this.createDelay;
    }

    public virtual bool IsAllResMax()
    {
        foreach (ResHolder resHolder in this.resHolders)
        {
            if (resHolder.IsMax() == false) return false;
        }

        return true;
    }

    public virtual List<Resource> TakeAll()
    {
        List<Resource> resources = new List<Resource>();
        foreach (ResHolder resHolder in this.resHolders)
        {
            Resource newResource = new Resource
            {
                name = resHolder.Name(),
                number = resHolder.TakeAll()
            };

            resources.Add(newResource);
        }

        return resources;
    }
}
