using UnityEngine;

public abstract class WorkerTask : SaiBehaviour
{
    [SerializeField] protected WorkerCtrl workerCtrl;
    [SerializeField] protected float buildingDistance = 0;
    [SerializeField] protected float buildDisLimit = 0.7f;

    //protected virtual void FixedUpdate()
    //{
    //    this.Working();
    //}

    public virtual void Working()
    {
        if (this.GetBuilding()) this.GettingReadyForWork();
        else this.FindBuildingForWorkder();

        if (workerCtrl.workerTasks.ReadyForTask) this.DoingTask();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
    }

    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = transform.parent.parent.GetComponent<WorkerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);
    }

    protected virtual void FindBuildingForWorkder()
    {
        BuildingHasWorkersCtrl buildingHasWorker = BuildingSpawnerCtrl.Instance.Manager.FindWorkStation();
        if (buildingHasWorker == null) return;
        buildingHasWorker.Workers.AddWorker(this.workerCtrl);
        this.AssignBuilding(buildingHasWorker);
    }

    public virtual void GotoBuilding()
    {
        BuildingCtrl buildingCtrl = this.GetBuilding();
        this.workerCtrl.workerMovement.SetTarget(buildingCtrl.door);
    }

    public virtual bool IsAtBuilding()
    {
        return this.BuildingDistance() < this.buildDisLimit;
    }

    protected virtual float BuildingDistance()
    {
        BuildingCtrl buildingCtrl = this.GetBuilding();
        this.buildingDistance = Vector3.Distance(transform.position, buildingCtrl.door.transform.position);
        return this.buildingDistance;
    }

    protected virtual void GettingReadyForWork()
    {
        if (this.workerCtrl.workerTasks.ReadyForTask) return;

        if (!this.workerCtrl.workerMovement.IsCloseToTarget())
        {
            this.GotoBuilding();
            return;
        }

        this.workerCtrl.workerTasks.SetReadyForTask(true);
        this.GoIntoBuilding();
    }

    public virtual void GoIntoBuilding()
    {
        if (this.workerCtrl.workerTasks.InHouse) return;

        this.workerCtrl.workerMovement.SetTarget(null);
        this.workerCtrl.workerTasks.SetInHouse(true);
        this.workerCtrl.workerModel.gameObject.SetActive(false);
    }

    public virtual void GoOutBuilding()
    {
        if (!this.workerCtrl.workerTasks.InHouse) return;

        this.workerCtrl.workerTasks.SetInHouse(false);
        this.workerCtrl.workerModel.gameObject.SetActive(true);
    }

    protected virtual void DoingTask()
    {
        this.GetBuilding().BuildingTask.DoingTask(this.workerCtrl);
    }

    protected virtual BuildingHasWorkersCtrl GetBuilding()
    {
        //For overide
        return null;
    }

    protected virtual void AssignBuilding(BuildingHasWorkersCtrl buildingCtrl)
    {
        //For overide
    }

    protected virtual BuildingType GetBuildingType()
    {
        return BuildingType.workStation;
    }
}
