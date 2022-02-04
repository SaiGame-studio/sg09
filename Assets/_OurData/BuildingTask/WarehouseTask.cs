using System.Collections.Generic;
using UnityEngine;

public class WarehouseTask : BuildingTask
{
    //[Header("Warehouse")]

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        switch (workerCtrl.workerTasks.TaskCurrent())
        {
            case TaskType.getResNeed2Move:
                this.GoGetResNeed2Move(workerCtrl);
                break;
            case TaskType.bringResourceBack:
                this.BringResourceBack(workerCtrl);
                break;
            case TaskType.goToWorkStation:
                this.BackToWorkStation(workerCtrl);
                break;
            default:
                if (this.IsTime2Work()) this.Planning(workerCtrl);
                break;
        }
    }

    protected virtual void Planning(WorkerCtrl workerCtrl)
    {
        BuildingCtrl buildingCtrl = this.GetWorkStationHasResNeed2Move();
        if (buildingCtrl != null)
        {
            workerCtrl.workerTasks.taskBuildingCtrl = buildingCtrl;
            workerCtrl.workerMovement.SetTarget(null);
            workerCtrl.workerTasks.TaskAdd(TaskType.getResNeed2Move);
        }
    }

    protected virtual void GoGetResNeed2Move(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.inHouse) workerTasks.taskWorking.GoOutBuilding();

        BuildingCtrl taskBuildingCtrl = workerTasks.taskBuildingCtrl;
        ResHolder resHolder = taskBuildingCtrl.warehouse.ResNeed2Move();
        if (resHolder == null)
        {
            this.DoneGetResNeed2Move(workerCtrl);
            return;
        }

        if (workerCtrl.workerMovement.GetTarget() == null) workerCtrl.workerMovement.SetTarget(taskBuildingCtrl.door);

        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        float count = 1;
        resHolder.Deduct(count);
        workerCtrl.resCarrier.AddResource(resHolder.Name(), count);
        this.DoneGetResNeed2Move(workerCtrl);

        //Find what building need these Resources
        BuildingCtrl buildingCtrl = this.FindBuildingNeedRes(resHolder.Name());
        workerTasks.taskBuildingCtrl = buildingCtrl;
        workerTasks.TaskAdd(TaskType.bringResourceBack);
    }

    protected virtual void DoneGetResNeed2Move(WorkerCtrl workerCtrl)
    {
        workerCtrl.workerTasks.TaskCurrentDone();
        workerCtrl.workerTasks.taskBuildingCtrl = null;
        //workerCtrl.workerMovement.SetTarget(null);
    }

    protected virtual BuildingCtrl GetWorkStationHasResNeed2Move()
    {
        foreach (BuildingCtrl buildingCtrl in BuildingManager.instance.BuildingCtrls())
        {
            if (buildingCtrl.buildingType != BuildingType.workStation) continue;
            ResHolder resHolder = buildingCtrl.warehouse.ResNeed2Move();
            if (resHolder == null) continue;
            return buildingCtrl;
        }

        return null;
    }

    protected virtual BuildingCtrl FindBuildingNeedRes(ResourceName resName)
    {
        foreach (BuildingCtrl buildingCtrl in BuildingManager.instance.BuildingCtrls())
        {
            if (buildingCtrl.buildingType != BuildingType.workStation) continue;
            ResHolder resHolder = buildingCtrl.warehouse.IsNeedRes(resName);
            if (resHolder == null) continue;
            return buildingCtrl;
        }

        return this.buildingCtrl;
    }

    protected virtual void BringResourceBack(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.inHouse) workerTasks.taskWorking.GoOutBuilding();

        BuildingCtrl taskBuildingCtrl = workerTasks.taskBuildingCtrl;
        if (workerCtrl.workerMovement.GetTarget() == null) workerCtrl.workerMovement.SetTarget(taskBuildingCtrl.door);

        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        //workerCtrl.workerMovement.SetTarget(null);
        workerTasks.taskBuildingCtrl = null;
        workerTasks.TaskCurrentDone();

        Resource res = workerCtrl.resCarrier.TakeFirst();
        taskBuildingCtrl.warehouse.AddResource(res.name,res.number);

        ResHolder resHolder = taskBuildingCtrl.warehouse.ResNeed2Move();
        if (resHolder == null) return;
        workerTasks.taskBuildingCtrl = taskBuildingCtrl;
        workerTasks.TaskAdd(TaskType.getResNeed2Move);
    }
}
