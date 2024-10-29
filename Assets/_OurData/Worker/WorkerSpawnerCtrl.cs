using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSpawnerCtrl : SaiSingleton<WorkerSpawnerCtrl>
{
    [SerializeField] protected WorkerSpawner spawner;
    public WorkerSpawner Spawner => spawner;

    [SerializeField] protected WorkerManager manager;
    public WorkerManager Manager => manager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerSpawner();
        this.LoadWorkerManager();
    }

    protected virtual void LoadWorkerManager()
    {
        if (this.manager != null) return;
        this.manager = GetComponent<WorkerManager>();
        Debug.Log(transform.name + ": LoadWorkerManager", gameObject);
    }

    protected virtual void LoadWorkerSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<WorkerSpawner>();
        Debug.Log(transform.name + " LoadWorkerSpawner", gameObject);
    }
}
