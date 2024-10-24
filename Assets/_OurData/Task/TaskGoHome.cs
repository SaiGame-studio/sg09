using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGoHome : WorkerTask
{
    protected override BuildingHasWorkersCtrl GetBuilding()
    {
        return this.workerCtrl.buildings.GetHome();
    }

    protected override void AssignBuilding(BuildingHasWorkersCtrl buildingCtrl)
    {
        this.workerCtrl.buildings.AssignHome(buildingCtrl);
    }

    protected override BuildingType GetBuildingType()
    {
        return BuildingType.home;
    }
}
