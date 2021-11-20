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
}
