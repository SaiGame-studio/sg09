using UnityEngine;

public class WorkerTasks : SaiBehaviour
{
    public WorkerCtrl workerCtrl;
    [SerializeField] protected bool gotoWork = false;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        this.WorkFinding();
        this.GotoWork();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerCtrl();
    }

    protected virtual void LoadWorkerCtrl()
    {
        if (this.workerCtrl != null) return;
        this.workerCtrl = GetComponent<WorkerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerCtrl", gameObject);
    }

    protected virtual void WorkFinding()
    {
        if (this.workerCtrl.workerBuildings.GetWork() != null) return;

        BuildingCtrl buildingCtrl = BuildingManager.instance.FindBuilding(transform);
        if (buildingCtrl == null) return;
        this.workerCtrl.workerBuildings.AssignWork(buildingCtrl);
    }

    protected virtual void GotoWork()
    {
        if (this.gotoWork == false) return;

        BuildingCtrl workBuilding = this.workerCtrl.workerBuildings.GetWork();
        if (workBuilding == null) return;
        this.workerCtrl.workerMovement.SetTarget(workBuilding.door);
    }
}
