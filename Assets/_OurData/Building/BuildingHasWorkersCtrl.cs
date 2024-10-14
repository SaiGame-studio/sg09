using UnityEngine;

public abstract class BuildingHasWorkersCtrl: BuildingCtrl
{
    [Header("Has Workers")]
    public Workers workers;
    public BuildingTask buildingTask;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkers();
        this.LoadBuldingTask();
    }

    protected virtual void LoadWorkers()
    {
        if (this.workers != null) return;
        this.workers = GetComponent<Workers>();
        Debug.Log(transform.name + ": LoadWorkers", gameObject);
    }

    protected virtual void LoadBuldingTask()
    {
        if (this.buildingTask != null) return;
        this.buildingTask = GetComponent<BuildingTask>();
        Debug.Log(transform.name + ": LoadBuldingTask", gameObject);
    }
}
