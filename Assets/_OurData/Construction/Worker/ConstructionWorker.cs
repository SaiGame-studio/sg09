using UnityEngine;

public class ConstructionWorker : AbsConstructFromPool<WorkerCtrl>
{
    protected override void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GameObject.Find("WorkerSpawner").GetComponent<WorkerSpawner>();
        Debug.LogWarning(transform.name + ": Load BuildWorker Spawner", gameObject);
    }
}
