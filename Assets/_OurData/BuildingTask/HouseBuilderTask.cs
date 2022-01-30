using System.Collections.Generic;
using UnityEngine;

public class HouseBuilderTask : BuildingTask
{
    [Header("House Builder")]
    [SerializeField] protected AbstractConstruction construction;
    [SerializeField] protected List<BuildingCtrl> warehouses;

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        switch (workerCtrl.workerTasks.TaskCurrent())
        {
            case TaskType.getResNeed2Move:
                Debug.Log("getResNeed2Move");
                break;
            default:
                if (this.IsTime2Work()) this.Planning(workerCtrl);
                break;
        }
    }

    protected virtual void Planning(WorkerCtrl workerCtrl)
    {
        this.construction = ConstructionManager.instance.GetConstruction();

        if (this.construction)
        {
            workerCtrl.workerTasks.TaskAdd(TaskType.getResNeed2Move);
            this.FindWarehouse();
        }
    }

    protected virtual void FindWarehouse()
    {
        List<BuildingCtrl> buildingCtrls = BuildingManager.instance.BuildingCtrls();
        foreach (BuildingCtrl buildingCtrl in buildingCtrls)
        {
            if (buildingCtrl.buildingTask.GetType() != typeof(WarehouseTask)) continue;
            this.warehouses.Add(buildingCtrl);
        }
    }
}
