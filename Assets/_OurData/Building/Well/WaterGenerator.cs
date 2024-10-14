using UnityEngine;

public class WaterGenerator : ResGenerator
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadResCreate();
        this.SetLimit();
    }

    protected virtual void LoadResCreate()
    {
        Resource res = new(ResourceName.water, 1);
        this.resCreate.Clear();
        this.resCreate.Add(res);
    }

    protected virtual void SetLimit()
    {
        ResHolder resHolder = this.GetRes(ResourceName.water);
        resHolder.SetLimit(7);
    }
}
