using UnityEngine;

public class BuildingTask : SaiBehaviour
{
    [Header("Building Task")]
    public BuildingCtrl buildingCtrl;
    [SerializeField] protected float taskTimer = 0;
    [SerializeField] protected float taskDelay = 5f;
    [SerializeField] protected float workingSpeed = 7;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingCtrl();
    }

    protected virtual void LoadBuildingCtrl()
    {
        if (this.buildingCtrl != null) return;
        this.buildingCtrl = GetComponent<BuildingCtrl>();
        Debug.Log(transform.name + " LoadBuildingCtrl", gameObject);
    }

    protected virtual bool IsTime2Work()
    {
        this.taskTimer += Time.fixedDeltaTime;
        if (this.taskTimer < this.taskDelay) return false;
        this.taskTimer = 0;
        return true;
    }

    protected virtual void BackToWorkStation(WorkerCtrl workerCtrl)
    {
        WorkerTask taskWorking = workerCtrl.workerTasks.taskWorking;
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