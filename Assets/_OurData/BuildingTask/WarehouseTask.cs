using System.Collections.Generic;
using UnityEngine;

public class WarehouseTask : BuildingTask
{
    [Header("Warehouse")]
    [SerializeField] protected int takeProductCount = 0;
    [SerializeField] protected int takeProductMax = 7;
    [SerializeField] protected float takeProductTimer = 0;
    [SerializeField] protected float takeProductDelay = 7f;
    [SerializeField] protected int giveMaterialCount = 0;
    [SerializeField] protected int giveMaterialMax = 2;
    [SerializeField] protected float giveMaterialTimer = 0;
    [SerializeField] protected float giveMaterialDelay = 7f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

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
            case TaskType.bringMatetiral2Building:
                this.BringMatetiral2Building(workerCtrl);
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
        workerCtrl.workerTasks.TaskAdd(TaskType.findBuildingNeedMaterial);
        workerCtrl.workerTasks.TaskAdd(TaskType.findBuildingHasProduct);

        this.giveMaterialCount = this.giveMaterialMax;
        this.takeProductCount = this.takeProductMax;
    }

    protected virtual void FindBuildingHasProduct(WorkerCtrl workerCtrl)
    {
        this.takeProductTimer += Time.fixedDeltaTime;
        if (this.takeProductTimer > this.takeProductDelay)
        {
            this.takeProductCount--;
            this.takeProductTimer = 0;
        }

        if (this.takeProductCount < 0)
        {
            workerCtrl.workerTasks.TaskCurrentDone();
            return;
        }

        List<Resource> resources;
        List<Resource> filterResources;
        foreach (BuildingCtrl buildingCtrl in this.ctrl.NearBuildings)
        {
            if (buildingCtrl.transform == transform) continue;
            if (buildingCtrl.warehouse == null) continue;
            resources = buildingCtrl.warehouse.ResNeed2Move();
            filterResources = this.RemoveResourceAlreadyFull(resources);
            if (filterResources.Count > 0)
            {
                workerCtrl.workerTasks.taskBuildingCtrl = buildingCtrl;
                workerCtrl.workerTasks.TaskAdd(TaskType.gotoGetProduct);
                this.takeProductTimer = 0;
                this.takeProductCount--;
                return;
            }
        }
    }

    protected virtual List<Resource> RemoveResourceAlreadyFull(List<Resource> resources)
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
        this.giveMaterialTimer += Time.fixedDeltaTime;
        if (this.giveMaterialTimer > this.giveMaterialDelay)
        {
            this.giveMaterialCount--;
            this.giveMaterialTimer = 0;
        }

        if (this.giveMaterialCount < 0)
        {
            workerCtrl.workerTasks.TaskCurrentDone();
            return;
        }

        List<Resource> resources;
        Resource currentRes;
        int carryCount = workerCtrl.resCarrier.carryCount;

        foreach (BuildingCtrl buildingCtrl in this.ctrl.NearBuildings)
        {
            if (buildingCtrl.BuildingType != BuildingType.workStation) continue;
            resources = buildingCtrl.warehouse.NeedResoures();
            foreach (Resource resource in resources)
            {
                currentRes = this.ctrl.warehouse.GetResource(resource.CodeName);
                if (currentRes.Number < 1) continue;

                this.ctrl.warehouse.RemoveResource(resource.CodeName, carryCount);
                workerCtrl.resCarrier.AddResource(resource.CodeName, carryCount);

                workerCtrl.workerTasks.taskBuildingCtrl = buildingCtrl;
                workerCtrl.workerTasks.TaskAdd(TaskType.bringMatetiral2Building);

                this.giveMaterialCount--;
                this.giveMaterialTimer = 0;
                return;
            }
        }
    }

    protected virtual void GotoGetProduct(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.InHouse) workerTasks.TaskWorking.GoOutBuilding();

        BuildingCtrl taskBuildingCtrl = workerTasks.taskBuildingCtrl;
        List<Resource> resourcesNeed2Move = taskBuildingCtrl.warehouse.ResNeed2Move();
        if (resourcesNeed2Move.Count == 0)
        {
            this.DoneGetResNeed2Move(workerCtrl);
            return;
        }

        if (workerCtrl.workerMovement.GetTarget() == null) workerCtrl.workerMovement.SetTarget(taskBuildingCtrl.door);
        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        int canCarry = workerCtrl.resCarrier.carryCount;
        Resource resInWarehouse;
        foreach (Resource resourceNeed2Move in resourcesNeed2Move)
        {
            resInWarehouse = this.ctrl.warehouse.GetResource(resourceNeed2Move.CodeName);
            if (resInWarehouse.IsMax()) continue;

            int taking = resourceNeed2Move.Number;
            if (taking > canCarry) taking = canCarry;
            else canCarry -= taking;

            resourceNeed2Move.Remove(taking);
            workerCtrl.resCarrier.AddResource(resourceNeed2Move.CodeName, taking);
        }
        this.DoneGetResNeed2Move(workerCtrl);

        //Find what building need these Resources
        workerTasks.taskBuildingCtrl = this.ctrl;
        workerTasks.TaskAdd(TaskType.takingProductBack);
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
        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        workerTasks.taskBuildingCtrl = null;
        workerTasks.TaskCurrentDone();

        List<Resource> resources = workerCtrl.resCarrier.TakeAll();
        foreach (Resource resource in resources)
        {
            taskBuildingCtrl.warehouse.AddResource(resource.CodeName, resource.Number);
        }

        workerTasks.TaskAdd(TaskType.goToWorkStation);
    }

    protected virtual void BringMatetiral2Building(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.InHouse) workerTasks.TaskWorking.GoOutBuilding();

        BuildingCtrl taskBuildingCtrl = workerTasks.taskBuildingCtrl;

        if (workerCtrl.workerMovement.GetTarget() == null) workerCtrl.workerMovement.SetTarget(taskBuildingCtrl.door);
        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        List<Resource> resources = workerCtrl.resCarrier.TakeAll();
        foreach (Resource resource in resources)
        {
            taskBuildingCtrl.warehouse.AddResource(resource.CodeName, resource.Number);
        }

        workerTasks.taskBuildingCtrl = null;
        workerTasks.TaskCurrentDone();

        workerTasks.TaskAdd(TaskType.goToWorkStation);
    }
}
