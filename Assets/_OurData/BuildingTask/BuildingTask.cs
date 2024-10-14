using UnityEngine;

public class BuildingTask : BuildingAbstract
{
    [Header("Building Task")]
    [SerializeField] protected float taskTimer = 0;
    [SerializeField] protected float taskDelay = 5f;
    [SerializeField] protected float workingSpeed = 7;
    [SerializeField] protected int lastBuildingWorked = 0;

    protected virtual bool IsTime2Work()
    {
        this.taskTimer += Time.fixedDeltaTime;
        if (this.taskTimer < this.taskDelay) return false;
        this.taskTimer = 0;
        return true;
    }

    protected virtual void GoToWorkStation(WorkerCtrl workerCtrl)
    {
        WorkerTask taskWorking = workerCtrl.workerTasks.TaskWorking;
        taskWorking.GotoBuilding();
        if (workerCtrl.workerMovement.IsClose2Target())
        {
            taskWorking.GoIntoBuilding();
            workerCtrl.workerTasks.TaskCurrentDone();
        }
    }

    public virtual void DoingTask(WorkerCtrl workerCtrl)
    {
        //For override
    }
}