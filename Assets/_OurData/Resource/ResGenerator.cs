using System.Collections.Generic;
using UnityEngine;

public class ResGenerator : Warehouse
{
    [Header("ResGenerator")]
    [SerializeField] protected List<Resource> resCreate;
    [SerializeField] protected List<Resource> resRequire;
    [SerializeField] protected bool canCreate = true;
    [SerializeField] protected float createTimer = 0f;
    [SerializeField] protected float createDelay = 7f;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.Reborn();
    }

    protected virtual void FixedUpdate() {
        this.Generating();
    }

    public virtual void Generating()
    {
        if (!this.canCreate) return;

        this.createTimer += this.GetElapsedTime();
        if (this.createTimer < this.createDelay) return;
        this.createTimer = 0;

        if (this.IsAllResMax()) return;
        if (!this.IsRequireEnough()) return;

        foreach (Resource res in this.resCreate)
        {
            Resource resource = this.GetResource(res.CodeName);
            resource.Add(res.Number);
        }
    }

    protected virtual bool IsRequireEnough()
    {
        if (this.resRequire.Count < 1) return true;

        //TODO: need to check require for each resource
        return false;
    }

    public virtual float GetCreateDelay()
    {
        return this.createDelay;
    }

    public virtual bool IsAllResMax()
    {
        foreach (Resource resource in this.resources)
        {
            if (resource.IsMax() == false) return false;
        }

        return true;
    }

    public virtual List<Resource> TakeAll()
    {
        List<Resource> resources = new();
        foreach (Resource res in this.resources)
        {
            Resource newResource = new(res.CodeName, res.TakeAll());
            resources.Add(newResource);
        }

        return resources;
    }

    protected virtual void Reborn()
    {
        this.canCreate = true;
    }
}
