using System.Collections.Generic;
using UnityEngine;

public class WarehouseTask : BuildingTask
{
    //[Header("Warehouse")]

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        switch (workerCtrl.workerTasks.TaskCurrent())
        {
            case TaskType.findBuildingHasProduct:
                this.FindBuildingHasProduct(workerCtrl);
                break;
            case TaskType.gotoGetProduct:
                this.GotoGetProduct(workerCtrl);
                break;
            case TaskType.takingProductBack:
                this.BringResourceBack(workerCtrl);
                break;
            case TaskType.findBuildingNeedMaterial:
                this.FindBuildingNeedMaterial(workerCtrl);
                break;
            case TaskType.bringMatetiralToBuilding:
                this.BringMatetiralToBuilding(workerCtrl);
                break;
            case TaskType.goToWorkStation:
                this.GoToWorkStation(workerCtrl);
                break;
            default:
                this.Planning(workerCtrl);
                break;
        }
    }

    protected virtual void Planning(WorkerCtrl workerCtrl)
    {
        workerCtrl.workerTasks.TaskAdd(TaskType.findBuildingNeedMaterial);
        workerCtrl.workerTasks.TaskAdd(TaskType.findBuildingHasProduct);
    }

    protected virtual void FindBuildingHasProduct(WorkerCtrl workerCtrl)
    {
        List<Resource> resources;
        List<Resource> filterResources;
        foreach (BuildingCtrl buildingCtrl in this.ctrl.NearBuildings)
        {
            if (buildingCtrl.transform == transform) continue;
            if (buildingCtrl.warehouse == null) continue;

            resources = buildingCtrl.warehouse.ResNeedToMove(workerCtrl, false);

            filterResources = this.FilterResourceAlreadyFull(resources);
            if (filterResources.Count > 0)
            {
                workerCtrl.workerTasks.TaskCurrentDone();

                workerCtrl.workerTasks.taskBuildingCtrl = buildingCtrl;
                workerCtrl.workerTasks.TaskAdd(TaskType.gotoGetProduct);

                buildingCtrl.warehouse.WillDeduct(filterResources);
                this.ctrl.warehouse.WillAdd(filterResources);
                return;
            }
        }

        workerCtrl.workerTasks.TaskCurrentDone();
    }

    protected virtual List<Resource> FilterResourceAlreadyFull(List<Resource> resources)
    {
        List<Resource> filterResources = new();
        Resource resourceInWarehouse;
        foreach (Resource resource in resources)
        {
            resourceInWarehouse = this.ctrl.warehouse.GetResource(resource.CodeName);
            if (resourceInWarehouse.IsMax()) continue;
            filterResources.Add(resource);
        }
        return filterResources;
    }

    protected virtual void FindBuildingNeedMaterial(WorkerCtrl workerCtrl)
    {
        List<Resource> resources;
        Resource currentRes;
        int carryCount = workerCtrl.inventory.CarryCount;

        foreach (BuildingCtrl buildingCtrl in this.ctrl.NearBuildings)
        {
            if (buildingCtrl.BuildingType != BuildingType.workStation) continue;
            resources = buildingCtrl.warehouse.NeedResoures();
            foreach (Resource resource in resources)
            {
                currentRes = this.ctrl.warehouse.GetResource(resource.CodeName);
                if (currentRes.NumberFinal() < 1) continue;

                this.ctrl.warehouse.RemoveResource(resource.CodeName, carryCount);
                workerCtrl.inventory.AddResource(resource.CodeName, carryCount);

                int carring = resource.Number;
                if (carring > carryCount) carring = carryCount;
                buildingCtrl.warehouse.WillAdd(resource.CodeName, carring);

                workerCtrl.workerTasks.taskBuildingCtrl = buildingCtrl;
                workerCtrl.workerTasks.TaskAdd(TaskType.bringMatetiralToBuilding);

                return;
            }
        }
        workerCtrl.workerTasks.TaskCurrentDone();
    }

    protected virtual void GotoGetProduct(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.InHouse) workerTasks.TaskWorking.GoOutBuilding();

        BuildingCtrl taskBuildingCtrl = workerTasks.taskBuildingCtrl;

        if (workerCtrl.workerMovement.GetTarget() == null) workerCtrl.workerMovement.SetTarget(taskBuildingCtrl.door);
        if (!workerCtrl.workerMovement.IsCloseToTarget()) return;

        List<Resource> resourcesNeed2Move = taskBuildingCtrl.warehouse.ResNeedToMove(workerCtrl, true);
        this.TakingResources(workerCtrl, taskBuildingCtrl, resourcesNeed2Move);
        this.DoneGetResNeed2Move(workerCtrl);

        workerTasks.taskBuildingCtrl = this.ctrl;
        workerTasks.TaskAdd(TaskType.takingProductBack);
    }

    protected virtual void TakingResources(WorkerCtrl workerCtrl, BuildingCtrl taskBuildingCtrl, List<Resource>  resourcesNeed2Move)
    {
        int canCarry = workerCtrl.inventory.CarryCount;

        foreach (Resource resourceNeed2Move in resourcesNeed2Move)
        {
            int taking = resourceNeed2Move.Number;
            if (taking > canCarry) taking = canCarry;
            else canCarry -= taking;

            taskBuildingCtrl.warehouse.RemoveResource(resourceNeed2Move.CodeName, taking);
            taskBuildingCtrl.warehouse.Deducted(resourceNeed2Move.CodeName, taking);

            workerCtrl.inventory.AddResource(resourceNeed2Move.CodeName, taking);
        }
    }

    protected virtual void DoneGetResNeed2Move(WorkerCtrl workerCtrl)
    {
        workerCtrl.workerTasks.TaskCurrentDone();
        workerCtrl.workerTasks.taskBuildingCtrl = null;
    }

    protected virtual void BringResourceBack(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.InHouse) workerTasks.TaskWorking.GoOutBuilding();

        BuildingCtrl taskBuildingCtrl = workerTasks.taskBuildingCtrl;
        if (workerCtrl.workerMovement.GetTarget() == null) workerCtrl.workerMovement.SetTarget(taskBuildingCtrl.door);
        if (!workerCtrl.workerMovement.IsCloseToTarget()) return;

        workerTasks.taskBuildingCtrl = null;
        workerTasks.TaskCurrentDone();

        List<Resource> resources = workerCtrl.inventory.TakeAll();
        foreach (Resource resource in resources)
        {
            taskBuildingCtrl.warehouse.AddResource(resource.CodeName, resource.Number);
            taskBuildingCtrl.warehouse.Added(resource.CodeName, resource.Number);
        }

        workerTasks.TaskAdd(TaskType.goToWorkStation);
    }

    protected virtual void BringMatetiralToBuilding(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.InHouse) workerTasks.TaskWorking.GoOutBuilding();

        BuildingCtrl taskBuildingCtrl = workerTasks.taskBuildingCtrl;

        if (workerCtrl.workerMovement.GetTarget() == null) workerCtrl.workerMovement.SetTarget(taskBuildingCtrl.door);
        if (!workerCtrl.workerMovement.IsCloseToTarget()) return;

        List<Resource> resources = workerCtrl.inventory.TakeAll();
        foreach (Resource resource in resources)
        {
            taskBuildingCtrl.warehouse.AddResource(resource.CodeName, resource.Number);
            taskBuildingCtrl.warehouse.Added(resource.CodeName, resource.Number);
        }

        workerTasks.taskBuildingCtrl = null;
        workerTasks.TaskCurrentDone();

        workerTasks.TaskAdd(TaskType.goToWorkStation);
    }
}
