using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResHolder : SaiBehaviour
{
    [SerializeField] protected ResourceName resourceName;
    [SerializeField] protected float resCurrent = 0;
    [SerializeField] protected float resMax = Mathf.Infinity;

    protected override void LoadComponents()
    {
        this.LoadResName();
    }

    protected virtual void LoadResName()
    {
        if (this.resourceName != ResourceName.noResource) return;

        string name = transform.name;
        this.resourceName = ResNameParser.FromString(name);
        Debug.Log(transform.name + ": LoadResName");
    }

    public virtual ResourceName Name()
    {
        return this.resourceName;
    }

    public virtual float Add(float number)
    {
        this.resCurrent += number;

        if (this.resCurrent > this.resMax) this.resCurrent = this.resMax;
        return this.resCurrent;
    }

    public virtual float Current()
    {
        return this.resCurrent;
    }

    public virtual float TakeAll()
    {
        float take = this.resCurrent;
        this.resCurrent = 0;
        return take;
    }

    public virtual void SetLimit(float max)
    {
        this.resMax = max;
    }

    public virtual bool IsMax()
    {
        return this.resCurrent == this.resMax;
    }
}
