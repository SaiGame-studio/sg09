using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workers : SaiBehaviour
{
    [SerializeField] protected int maxWorker = 1;
    [SerializeField] protected List<Transform> workers;

    protected override void LoadComponents()
    {
        this.LoadWorkers();
    }

    protected virtual void LoadWorkers()
    {
        if (this.workers.Count > 0) return;

        Transform workers = transform.Find("Workers");
        foreach (Transform worker in workers)
        {
            this.workers.Add(worker);
        }

        Debug.Log(transform.name + ": LoadWorkers");
    }

    public virtual bool IsNeedWorker()
    {
        if (this.workers.Count >= this.maxWorker) return false;
        return true;
    }

    public virtual void AddWorker(Transform worker)
    {
        this.workers.Add(worker);
    }
}
