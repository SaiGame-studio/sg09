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
            case TaskType.findWarehouseHasRes:
                this.FindWarehouseHasRes(workerCtrl);
                break;
            case TaskType.getResNeed2Move:
                this.GetResNeed2Move(workerCtrl);
                break;
            case TaskType.bringResourceBack:
                this.BringResToConstruction(workerCtrl);
                break;
            case TaskType.buildConstruction:
                this.BuildConstruction(workerCtrl);
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
            workerCtrl.workerTasks.TaskAdd(TaskType.findWarehouseHasRes);
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

    protected virtual void FindWarehouseHasRes(WorkerCtrl workerCtrl)
    {
        ResourceName resRequireName = this.construction.GetResRequireName();
        if(resRequireName == ResourceName.noResource)
        {
            workerCtrl.workerMovement.SetTarget(null);
            workerCtrl.workerTasks.TaskCurrentDone();
            workerCtrl.workerTasks.TaskAdd(TaskType.buildConstruction);
            return;
        }

        foreach (BuildingCtrl warehouse in this.warehouses)
        {
            ResHolder resHolder = warehouse.warehouse.GetResource(resRequireName);
            if (resHolder.Current() < 1) continue;
            workerCtrl.workerTasks.taskBuildingCtrl = warehouse;
            workerCtrl.workerTasks.TaskAdd(TaskType.getResNeed2Move);
            return;
        }
    }

    protected virtual void GetResNeed2Move(WorkerCtrl workerCtrl)
    {
        BuildingCtrl warehouseCtrl = workerCtrl.workerTasks.taskBuildingCtrl;

        ResourceName resRequireName = this.construction.GetResRequireName();
        ResHolder resHolder = warehouseCtrl.warehouse.GetResource(resRequireName);
        if (resHolder.Current() < 1)
        {
            workerCtrl.workerTasks.TaskCurrentDone();
            return;
        }

        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.inHouse) workerTasks.taskWorking.GoOutBuilding();

        Transform target = workerCtrl.workerMovement.GetTarget();
        if (target == null) workerCtrl.workerMovement.SetTarget(warehouseCtrl.door);

        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        int carryCount = workerCtrl.resCarrier.carryCount;
        warehouseCtrl.warehouse.RemoveResource(resRequireName, carryCount);
        workerCtrl.resCarrier.AddResource(resRequireName, carryCount);
        
        workerCtrl.workerTasks.TaskCurrentDone();
        workerCtrl.workerMovement.SetTarget(null);
        workerCtrl.workerTasks.TaskAdd(TaskType.bringResourceBack);
    }

    protected virtual void BringResToConstruction(WorkerCtrl workerCtrl)
    {
        Transform target = workerCtrl.workerMovement.GetTarget();
        if (target == null) workerCtrl.workerMovement.SetTarget(this.construction.transform);

        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        workerCtrl.workerMovement.SetTarget(null);

        workerCtrl.workerTasks.TaskCurrentDone();

        Resource res = workerCtrl.resCarrier.TakeFirst();
        this.construction.AddRes(res.name, res.number);
    }

    protected virtual void BuildConstruction(WorkerCtrl workerCtrl)
    {
        Debug.Log("BuildConstruction");
    }
}
