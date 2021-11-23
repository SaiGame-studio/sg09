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

    public virtual float Add(int number)
    {
        this.resCurrent += number;

        if (this.resCurrent > this.resMax) this.resCurrent = this.resMax;
        return this.resCurrent;
    }

}
