using UnityEngine;

public abstract class BuildingTask : BuildingAbstract
{
    [Header("Building Task")]
    [SerializeField] protected float workingSpeed = 7;
    public abstract void DoingTask(WorkerCtrl workerCtrl);

    protected virtual void GoToWorkStation(WorkerCtrl workerCtrl)
    {
        WorkerTask taskWorking = workerCtrl.workerTasks.TaskWorking;
        taskWorking.GotoBuilding();
        if (workerCtrl.workerMovement.IsCloseToTarget())
        {
            taskWorking.GoIntoBuilding();
            workerCtrl.workerTasks.TaskCurrentDone();
        }
    }

    
}