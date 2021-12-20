using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskWorking : WorkerTask
{
    protected override BuildingCtrl GetBuilding()
    {
        return this.workerCtrl.workerBuildings.GetWork();
    }

    protected override void AssignBuilding(BuildingCtrl buildingCtrl)
    {
        this.workerCtrl.workerBuildings.AssignWork(buildingCtrl);
    }

    protected override BuildingType GetBuildingType()
    {
        return BuildingType.workStation;
    }
}
