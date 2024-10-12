using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSpawnerCtrl : SaiSingleton<WorkerSpawnerCtrl>
{
    [SerializeField] protected WorkerSpawner spawner;
    public WorkerSpawner Spawner => spawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerSpawner();
    }

    protected virtual void LoadWorkerSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<WorkerSpawner>();
        Debug.Log(transform.name + " LoadWorkerSpawner", gameObject);
    }
}
