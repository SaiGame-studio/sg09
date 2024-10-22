using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSpawner : Spawner<WorkerCtrl>
{
    [SerializeField] protected WorkerSpawnerCtrl ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkerSpawnerCtrl();
    }

    protected virtual void LoadWorkerSpawnerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<WorkerSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadWorkerSpawnerCtrl", gameObject);
    }

    public override WorkerCtrl Spawn(WorkerCtrl prefab)
    {
        WorkerCtrl newObj = base.Spawn(prefab);
        this.ctrl.Manager.Add(newObj);
        return newObj;
    }
}
