using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionWorker : AbstractPoolConstruct<WorkerCtrl>
{
    //[Header("Worker")]

    protected override void ResetValues()
    {
        base.ResetValues();
        this.delay = 0.005f;
    }

    protected override void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GameObject.Find("WorkerSpawner").GetComponent<WorkerSpawner>();
        Debug.LogWarning(transform.name + ": Load BuildWorker Spawner", gameObject);
    }
}
