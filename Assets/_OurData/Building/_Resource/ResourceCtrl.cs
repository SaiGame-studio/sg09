using UnityEngine;

public abstract class ResourceCtrl : BuildingCtrl
{
    protected override void ResetValues()
    {
        base.ResetValues();
        this.buildingType = BuildingType.resource;
    }
}
