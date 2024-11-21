using System.Collections;
using UnityEngine;

public class SawmillTask : BuildingTask
{
    [Header("Sawmill")]
    [SerializeField] protected Transform workingPoint;
    [SerializeField] protected int logwoodCost = 1;
    [SerializeField] protected int blankReceive = 2;

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
                this.GoToWorkStation(workerCtrl);
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
        if (workerCtrl.workerMovement.IsWorking) return;
        StartCoroutine(Sawing(workerCtrl));
    }

    IEnumerator Sawing(WorkerCtrl workerCtrl)
    {
        workerCtrl.workerMovement.SetWorking(true);
        workerCtrl.workerMovement.SetWorkingType(WorkingType.sawing);
        yield return new WaitForSeconds(this.workingSpeed);

        this.ctrl.warehouse.RemoveResource(ResourceName.logwood, this.logwoodCost);
        this.ctrl.warehouse.AddResource(ResourceName.blank, this.blankReceive);

        workerCtrl.workerMovement.SetWorking(false);
        workerCtrl.workerTasks.TaskCurrentDone();
    }


    protected virtual void GotoWorkingPoint(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.InHouse) workerTasks.TaskWorking.GoOutBuilding();

        Transform target = workerCtrl.workerMovement.GetTarget();
        if (target == null) workerCtrl.workerMovement.SetTarget(this.workingPoint);

        if (!workerCtrl.workerMovement.IsCloseToTarget()) return;

        //workerCtrl.workerMovement.SetTarget(null);
        workerCtrl.workerTasks.TaskCurrentDone();
    }

    protected virtual bool IsStoreMax()
    {
        Resource resource = this.ctrl.warehouse.GetResource(ResourceName.blank);
        return resource.IsMax();
    }

    protected virtual bool HasLogwood()
    {
        Resource logwood = this.ctrl.warehouse.GetResource(ResourceName.logwood);
        return logwood.NumberFinal() > 0;
    }
}
