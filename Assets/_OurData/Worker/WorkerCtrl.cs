using UnityEngine;

public class WorkerCtrl : SaiBehaviour
{
    public WorkerBuildings workerBuildings;
    public WorkerMovement workerMovement;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerBuildings();
        this.LoadWorkerMovement();
    }

    protected virtual void LoadWorkerBuildings()
    {
        if (this.workerBuildings != null) return;
        this.workerBuildings = GetComponent<WorkerBuildings>();
        Debug.Log(transform.name + ": LoadWorkerBuildings", gameObject);
    }

    protected virtual void LoadWorkerMovement()
    {
        if (this.workerMovement != null) return;
        this.workerMovement = GetComponent<WorkerMovement>();
        Debug.Log(transform.name + ": LoadWorkerMovement", gameObject);
    }
}
