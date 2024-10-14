using UnityEngine;

public abstract class ResourceCtrl : BuildingCtrl
{
    //[Header("Building")]
    protected override void ResetValues()
    {
        base.ResetValues();
        this.buildingType = BuildingType.resource;
    }
}
