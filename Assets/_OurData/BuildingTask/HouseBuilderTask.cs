using System.Collections.Generic;
using UnityEngine;

public class HouseBuilderTask : BuildingTask
{
    [Header("House Builder")]
    [SerializeField] protected AbsConstruction construction;
    [SerializeField] protected List<BuildingCtrl> warehouses;

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        switch (workerCtrl.workerTasks.TaskCurrent())
        {
            case TaskType.findWarehouseHasRes:
                this.FindWarehouseHasResource(workerCtrl);
                break;
            case TaskType.getResNeed2Move:
                this.GetResNeed2Move(workerCtrl);
                break;
            case TaskType.bringResourceBack:
                this.BringResourceToConstruction(workerCtrl);
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
            this.construction.SetBuilder(this.ctrl);
            workerCtrl.workerTasks.TaskAdd(TaskType.findWarehouseHasRes);
            this.FindWarehouse();
        }
    }

    protected virtual void FindWarehouse()
    {
        List<BuildingCtrl> buildingCtrls = BuildingSpawnerCtrl.Instance.Manager.BuildingCtrls();
        foreach (BuildingCtrl buildingCtrl in buildingCtrls)
        {
            if (buildingCtrl.GetName() != BuildingName.Warehouse.ToString()) continue;
            if (this.warehouses.Contains(buildingCtrl)) continue;
            this.warehouses.Add(buildingCtrl);
        }
    }

    protected virtual void FindWarehouseHasResource(WorkerCtrl workerCtrl)
    {
        Resource resourceRequired = this.construction.GetResourceRequired();

        if (resourceRequired == null)
        {
            workerCtrl.workerTasks.TaskCurrentDone();
            workerCtrl.workerTasks.TaskAdd(TaskType.buildConstruction);
            return;
        }

        foreach (BuildingCtrl warehouse in this.warehouses)
        {
            Resource resourceInWarehouse = warehouse.warehouse.GetResource(resourceRequired.CodeName);
            if (resourceInWarehouse.NumberFinal() < 1) continue;

            workerCtrl.workerTasks.taskBuildingCtrl = warehouse;
            workerCtrl.workerTasks.TaskCurrentDone();
            workerCtrl.workerTasks.TaskAdd(TaskType.getResNeed2Move);

            int number = resourceRequired.Number;
            if (number > resourceInWarehouse.NumberFinal()) number = resourceInWarehouse.NumberFinal();

            int taking = workerCtrl.inventory.Taking(number);
            resourceInWarehouse.WillDeduct(taking);
            this.construction.WillAdd(resourceRequired.CodeName, taking);
            return;
        }
    }

    protected virtual void GetResNeed2Move(WorkerCtrl workerCtrl)
    {
        BuildingCtrl warehouseCtrl = workerCtrl.workerTasks.taskBuildingCtrl;

        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.InHouse) workerTasks.TaskWorking.GoOutBuilding();

        Transform target = workerCtrl.workerMovement.GetTarget();
        if (target == null) workerCtrl.workerMovement.SetTarget(warehouseCtrl.door);

        if (!workerCtrl.workerMovement.IsCloseToTarget()) return;

        Resource requestResource = this.construction.GetResourceRequired();
        int taking = workerCtrl.inventory.Taking(requestResource.Number);

        warehouseCtrl.warehouse.RemoveResource(requestResource.CodeName, taking);
        warehouseCtrl.warehouse.Deducted(requestResource.CodeName, taking);

        workerCtrl.inventory.AddResource(requestResource.CodeName, taking);

        workerCtrl.workerTasks.TaskCurrentDone();
        workerCtrl.workerTasks.TaskAdd(TaskType.bringResourceBack);
    }

    protected virtual void BringResourceToConstruction(WorkerCtrl workerCtrl)
    {
        Transform target = workerCtrl.workerMovement.GetTarget();
        if (target == null) workerCtrl.workerMovement.SetTarget(this.construction.transform);
        if (!workerCtrl.workerMovement.IsCloseToTarget()) return;

        workerCtrl.workerTasks.TaskCurrentDone();

        List<Resource> resources = workerCtrl.inventory.TakeAll();
        foreach (Resource resource in resources)
        {
            this.construction.AddRes(resource.CodeName, resource.Number);
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
        if (this.construction == null) return true;//TODO: not testing code

        float percent = this.construction.Percent();
        if (percent < 99) return false;

        this.construction.Finish();
        this.construction = null;
        return true;
    }
}
