using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingCtrl : PoolObj
{
    [Header("Building")]
    public Transform door;
    public Warehouse warehouse;
    [SerializeField] protected BuildingType buildingType = BuildingType.workStation;
    public BuildingType BuildingType => buildingType;
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
        this.LoadDoor();
        this.LoadWarehouse();
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
