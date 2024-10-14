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
        Resource water = this.GetResource(ResourceName.water);
        water.SetMax(7);
    }

    public override Resource ResNeed2Move()
    {
        Resource res = this.GetResource(ResourceName.water);
        if (res.Number > 2) return res;
        return null;
    }
}
