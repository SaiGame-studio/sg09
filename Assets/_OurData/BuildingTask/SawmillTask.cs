using UnityEngine;

public class SawmillTask : BuildingTask
{
    [Header("Sawmill")]
    [SerializeField] protected Transform workingPoint;
    [SerializeField] protected float workingSpeed = 7;

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
                Debug.Log("makingResource");
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
            workerCtrl.workerTasks.TaskAdd(TaskType.makingResource);
            workerCtrl.workerTasks.TaskAdd(TaskType.gotoWorkingPoint);
        }
    }

    protected virtual void GotoWorkingPoint(WorkerCtrl workerCtrl)
    {
        WorkerTasks workerTasks = workerCtrl.workerTasks;
        if (workerTasks.inHouse) workerTasks.taskWorking.GoOutBuilding();

        Transform target = workerCtrl.workerMovement.GetTarget();
        if (target == null) workerCtrl.workerMovement.SetTarget(this.workingPoint);

        if (!workerCtrl.workerMovement.IsClose2Target()) return;

        workerCtrl.workerMovement.SetTarget(null);
        workerCtrl.workerTasks.TaskCurrentDone();
    }

    protected virtual bool IsStoreMax()
    {
        return false;
    }

    protected virtual bool HasLogwood()
    {
        return true;
    }
}
