using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuilding : AbstractConstruction
{


    protected override Transform FinishBuild()
    {
        Transform newBuild = base.FinishBuild();
        BuildingCtrl buildingCtrl = newBuild.GetComponent<BuildingCtrl>();
        BuildingManager.instance.AddBuilding(buildingCtrl);
        return newBuild;
    }


    protected override void Building()
    {
        if (this.percent < 99) base.Building();
    }
}
