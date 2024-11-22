using UnityEngine;

public class BaseBuildingTask : BuildingTask
{

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    public override void DoingTask(WorkerCtrl workerCtrl)
    {
        switch (workerCtrl.workerTasks.TaskCurrent())
        {
            case TaskType.makingResource:
                Debug.Log("makingResource ");
                break;
            default:
                this.Planning(workerCtrl);
                break;
        }
    }

    protected virtual void Planning(WorkerCtrl workerCtrl)
    {

    }
}
