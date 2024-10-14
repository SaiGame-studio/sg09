using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGoHome : WorkerTask
{
    protected override BuildingHasWorkersCtrl GetBuilding()
    {
        return this.workerCtrl.workerBuildings.GetHome();
    }

    protected override void AssignBuilding(BuildingHasWorkersCtrl buildingCtrl)
    {
        this.workerCtrl.workerBuildings.AssignHome(buildingCtrl);
    }

    protected override BuildingType GetBuildingType()
    {
        return BuildingType.home;
    }
}
