using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : SaiSingleton<WorkerManager>
{
    [SerializeField] protected WorkerSpawnerCtrl ctrl;
    [SerializeField] protected int workingCount = 0;
    public int WorkingCount => workingCount;
    [SerializeField] protected List<WorkerCtrl> workers;
    public List<WorkerCtrl> Workers => workers;

    protected virtual void FixedUpdate()
    {
        this.CountWorking();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerSpawnerCtrl();
        this.LoadPoolObjects();
    }

    protected virtual void LoadWorkerSpawnerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<WorkerSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerSpawnerCtrl", gameObject);
    }

    protected virtual void LoadPoolObjects()
    {
        if (this.workers.Count > 0) return;
        WorkerCtrl[] components = this.ctrl.Spawner.PoolHolder.GetComponentsInChildren<WorkerCtrl>();
        this.workers = new List<WorkerCtrl>(components);
        Debug.Log(transform.name + ": LoadPoolObjects", gameObject);
    }

    protected virtual void CountWorking()
    {
        int working = 0;
        foreach(WorkerCtrl workerCtrl in this.workers)
        {
            if (workerCtrl.buildings.WorkBuilding == null) continue;
            working++;
        }
        this.workingCount = working;
    }

    public virtual void Add(WorkerCtrl ctrl)
    {
        this.workers.Add(ctrl);
    }
}
