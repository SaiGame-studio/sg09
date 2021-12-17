using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGoHome : WorkerTask
{

    protected override void Working()
    {
        Debug.Log(transform.parent.parent.name + " Working", gameObject);
    }

    protected override BuildingCtrl GetBuilding()
    {
        return this.workerCtrl.workerBuildings.GetHome();
    }

    protected override void AssignBuilding(BuildingCtrl buildingCtrl)
    {
        this.workerCtrl.workerBuildings.AssignHome(buildingCtrl);
    }

    protected override BuildingType GetBuildingType()
    {
        return BuildingType.home;
    }
}
