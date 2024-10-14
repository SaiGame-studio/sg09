using UnityEngine;

public class WellCtrl : BuildingCtrl
{
    protected override void ResetValues()
    {
        base.ResetValues();
        this.buildingType = BuildingType.resource;
    }

    public override string GetName()
    {
        return BuildingName.Well.ToString();
    }
}
