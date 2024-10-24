using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskWorking : WorkerTask
{
    protected override BuildingHasWorkersCtrl GetBuilding()
    {
        return this.workerCtrl.buildings.GetWork();
    }

    protected override void AssignBuilding(BuildingHasWorkersCtrl buildingCtrl)
    {
        this.workerCtrl.buildings.AssignWork(buildingCtrl);
    }

    protected override BuildingType GetBuildingType()
    {
        return BuildingType.workStation;
    }
}
