using System.Collections.Generic;
using UnityEngine;

public class HouseBuilderTask : BuildingTask
{
    [Header("House Builder")]
    [SerializeField] protected AbsConstruction construction;
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
            case TaskType.goToWorkStation:
                this.GoToWorkStation(workerCtrl);
                break;
            default:
                if (this.IsTime2Work()) this.Planning(workerCtrl);
                break;
        }
    }

    protected virtual void Planning(WorkerCtrl workerCtrl)
    {
        if (this.construction == null) this.construction = ConstructionManager.Instance.GetConstruction();

        if (this.construction)
        {
            this.construction.builder = this.buildingCtrl;
            workerCtrl.workerTasks.TaskAdd(TaskType.findWarehouseHasRes);
            this.FindWarehouse();
        }
    }

    protected virtual void FindWarehouse()
    {
        List<BuildingCtrl> buildingCtrls = BuildingSpawnerCtrl.Instance.Manager.BuildingCtrls();
        foreach (BuildingCtrl buildingCtrl in buildingCtrls)
        {
            if (buildingCtrl.buildingTask.GetType() != typeof(WarehouseTask)) continue;
            if (this.warehouses.Contains(buildingCtrl)) continue;
            this.warehouses.Add(buildingCtrl);
        }
    }

    protected virtual void FindWarehouseHasRes(WorkerCtrl workerCtrl)
    {
        ResourceName resRequireName = this.construction.GetResRequireName();

        foreach (BuildingCtrl warehouse in this.warehouses)
        {
            ResHolder resHolder = warehouse.warehouse.GetResource(resRequireName);
            if (resHolder.Current() < 1) continue;
            workerCtrl.workerTasks.taskBuildingCtrl = warehouse;
            workerCtrl.workerTasks.TaskCurrentDone();
            workerCtrl.workerTasks.TaskAdd(TaskType.getResNeed2Move);
            return;
        }
    }

    protected virtual void GetResNeed2Move(WorkerCtrl workerCtrl)
    {
        BuildingCtrl warehouseCtrl = workerCtrl.workerTasks.taskBuildingCtrl;

        ResourceName resRequireName = this.construction.GetResRequireName();
        ResHolder resHolder = warehouseCtrl.warehouse.GetResource(resRequireName);
        if (resHolder.Current() < 1)//TODO: not work with multi workers
        {
            workerCtrl.workerTasks.TaskCurrentDone();
            workerCtrl.workerTasks.TaskAdd(TaskType.findWarehouseHasRes);
            return;
        }

        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.inHouse) workerTasks.taskWorking.GoOutBuilding();

        Transform target = workerCtrl.workerMovement.GetTarget();
        if (target == null) workerCtrl.workerMovement.SetTarget(warehouseCtrl.door);

        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        workerCtrl.workerTasks.TaskCurrentDone();
        int carryCount = workerCtrl.resCarrier.carryCount;
        warehouseCtrl.warehouse.RemoveResource(resRequireName, carryCount);
        workerCtrl.resCarrier.AddResource(resRequireName, carryCount);
        workerCtrl.workerTasks.TaskAdd(TaskType.bringResourceBack);
    }

    protected virtual void BringResToConstruction(WorkerCtrl workerCtrl)
    {
        Transform target = workerCtrl.workerMovement.GetTarget();
        if (target == null) workerCtrl.workerMovement.SetTarget(this.construction.transform);
        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        workerCtrl.workerTasks.TaskCurrentDone();
        Resource res = workerCtrl.resCarrier.TakeFirst();
        this.construction.AddRes(res.name, res.number);

        ResourceName resRequireName = this.construction.GetResRequireName();
        if (resRequireName == ResourceName.noResource)
        {
            workerCtrl.workerTasks.TaskAdd(TaskType.buildConstruction);
            return;
        }

        workerCtrl.workerTasks.TaskAdd(TaskType.findWarehouseHasRes);
    }

    protected virtual void BuildConstruction(WorkerCtrl workerCtrl)
    {
        if (!this.IsConstructionFinish()) return;

        workerCtrl.workerTasks.TaskCurrentDone();
        workerCtrl.workerTasks.TaskAdd(TaskType.goToWorkStation);
        //TODO: add building animation
    }

    protected virtual bool IsConstructionFinish()
    {
        if (this.construction == null) return true;//TODO: untesting code

        float percent = this.construction.Percent();
        if (percent < 99) return false;

        this.construction.Finish();
        this.construction = null;
        return true;
    }
}
