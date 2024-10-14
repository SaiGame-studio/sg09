using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingCtrl : ResourceCtrl
{
    [Header("Building")]
    public Transform door;
    public Workers workers;
    public Warehouse warehouse;
    public BuildingTask buildingTask;
    [SerializeField] protected List<BuildingCtrl> nearBuildings;
    public List<BuildingCtrl> NearBuildings => nearBuildings;

    protected override void Start()
    {
        base.Start();
        this.FindNearBuildings();
    }

    protected override void ResetValues()
    {
        base.ResetValues();
        this.buildingType = BuildingType.workStation;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWorkers();
        this.LoadDoor();
        this.LoadWarehouse();
        this.LoadBuldingTask();
    }

    protected virtual void LoadWorkers()
    {
        if (this.workers != null) return;
        this.workers = GetComponent<Workers>();
        //Debug.LogWarning(transform.name + ": LoadWorkers", gameObject);
    }

    protected virtual void LoadDoor()
    {
        if (this.door != null) return;
        this.door = transform.Find("Door");
        Debug.LogWarning(transform.name + " LoadDoor", gameObject);
    }

    protected virtual void LoadWarehouse()
    {
        if (this.warehouse != null) return;
        this.warehouse = GetComponent<Warehouse>();
        Debug.LogWarning(transform.name + " LoadWarehouse", gameObject);
    }

    protected virtual void LoadBuldingTask()
    {
        if (this.buildingTask != null) return;
        this.buildingTask = GetComponent<BuildingTask>();
        //Debug.LogWarning(transform.name + ": LoadBuldingTask", gameObject);
    }

    public virtual void FindNearBuildings()
    {
        this.nearBuildings.Clear();
        this.nearBuildings = new List<BuildingCtrl>(BuildingSpawnerCtrl.Instance.Manager.BuildingCtrls());
        this.nearBuildings.Sort(delegate (BuildingCtrl a, BuildingCtrl b)
        {
            Vector3 aPos = a.transform.position;
            Vector3 bPos = b.transform.position;
            Vector3 currentPos = transform.position;
            return Vector3.Distance(currentPos, aPos)
            .CompareTo(Vector3.Distance(currentPos, bPos));
        });
        //Invoke("FindNearBuildings", 7f);
    }
}
