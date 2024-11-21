using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workers : SaiBehaviour
{
    [SerializeField] protected int maxWorker = 1;
    [SerializeField] protected List<WorkerCtrl> workers;
    //[SerializeField] protected List<Transform> workers;

    public virtual bool IsNeedWorker()
    {
        if (this.workers.Count >= this.maxWorker) return false;
        return true;
    }

    public virtual void AddWorker(WorkerCtrl worker)
    {
        this.workers.Add(worker);
    }

    public virtual void ReleaseWorkers()
    {
        foreach(WorkerCtrl workerCtrl in this.workers)
        {
            workerCtrl.WorkerReleased();
        }

        this.workers.Clear();
    }

    public virtual int Count()
    {
        return this.workers.Count;
    }
}
