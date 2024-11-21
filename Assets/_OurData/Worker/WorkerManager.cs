using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : SaiSingleton<WorkerManager>
{
    [SerializeField] protected WorkerSpawnerCtrl ctrl;
    
    [SerializeField] protected int workingCount = 0;
    public int WorkingCount => workingCount;

    [SerializeField] protected int workerCheckIndex = 0;
    [SerializeField] protected int workerCheckCount = 10;
    [SerializeField] protected List<WorkerCtrl> workers;
    public List<WorkerCtrl> Workers => workers;

    protected virtual void FixedUpdate()
    {
        this.WorkersChecking();
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

    public virtual void Add(WorkerCtrl ctrl)
    {
        this.workers.Add(ctrl);
    }

    protected virtual void WorkersChecking()
    {
        if (this.workers.Count <= 0) return;
        for (int i = 0; i < this.workerCheckCount; i++)
        {
            WorkerCtrl workerCtrl = this.workers[this.workerCheckIndex];
            workerCtrl.workerMovement.Moving();
            workerCtrl.workerTasks.Working();

            this.workerCheckIndex++;
            if (this.workerCheckIndex >= this.workers.Count)
            {
                this.workerCheckIndex = 0;
            }
        }
    }
}
