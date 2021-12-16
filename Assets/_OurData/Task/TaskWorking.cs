using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskWorking : WorkerTask
{

    protected override void Working()
    {
        Debug.Log(transform.parent.parent.name + " Working", gameObject);
    }

    protected override BuildingCtrl GetBuilding()
    {
        return this.workerCtrl.workerBuildings.GetWork();
    }

    protected override void AssignBuilding(BuildingCtrl buildingCtrl)
    {
        this.workerCtrl.workerBuildings.AssignWork(buildingCtrl);
    }
}
