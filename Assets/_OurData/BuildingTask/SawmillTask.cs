using System.Collections;
using UnityEngine;

public class SawmillTask : BuildingTask
{
    [Header("Sawmill")]
    [SerializeField] protected Transform workingPoint;
    [SerializeField] protected float logwoodCost = 1;
    [SerializeField] protected float blankReceive = 2;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkingPoint();
    }

    protected virtual void LoadWorkingPoint()
    {
        if (this.workingPoint != null) return;
        this.workingPoint = transform.Find("WorkingPoint");
        Debug.Log(transform.name + " LoadObjects", gameObject);
    }

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        switch (workerCtrl.workerTasks.TaskCurrent())
        {
            case TaskType.makingResource:
                this.MakingResource(workerCtrl);
                break;
            case TaskType.gotoWorkingPoint:
                this.GotoWorkingPoint(workerCtrl);
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
        if (!this.IsStoreMax() && this.HasLogwood())
        {
            workerCtrl.workerTasks.TaskAdd(TaskType.goToWorkStation);
            workerCtrl.workerTasks.TaskAdd(TaskType.makingResource);
            workerCtrl.workerTasks.TaskAdd(TaskType.gotoWorkingPoint);
        }
    }

    protected virtual void MakingResource(WorkerCtrl workerCtrl)
    {
        if (workerCtrl.workerMovement.isWorking) return;
        StartCoroutine(Sawing(workerCtrl));
    }

    IEnumerator Sawing(WorkerCtrl workerCtrl)
    {
        workerCtrl.workerMovement.isWorking = true;
        workerCtrl.workerMovement.workingType = WorkingType.sawing;
        yield return new WaitForSeconds(this.workingSpeed);

        this.buildingCtrl.warehouse.RemoveResource(ResourceName.logwood, this.logwoodCost);
        this.buildingCtrl.warehouse.AddResource(ResourceName.blank, this.blankReceive);

        workerCtrl.workerMovement.isWorking = false;
        workerCtrl.workerTasks.TaskCurrentDone();
    }


    protected virtual void GotoWorkingPoint(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.inHouse) workerTasks.taskWorking.GoOutBuilding();

        Transform target = workerCtrl.workerMovement.GetTarget();
        if (target == null) workerCtrl.workerMovement.SetTarget(this.workingPoint);

        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        //workerCtrl.workerMovement.SetTarget(null);
        workerCtrl.workerTasks.TaskCurrentDone();
    }

    protected virtual bool IsStoreMax()
    {
        ResHolder blank= this.buildingCtrl.warehouse.GetResource(ResourceName.blank);
        return blank.IsMax();
    }

    protected virtual bool HasLogwood()
    {
        ResHolder logwood = this.buildingCtrl.warehouse.GetResource(ResourceName.logwood);
        return logwood.Current() > 0;
    }
}
