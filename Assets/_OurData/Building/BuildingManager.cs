using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : SaiSingleton<BuildingManager>
{
    [SerializeField] protected BuildingSpawnerCtrl ctrl;
    [SerializeField] protected List<BuildingCtrl> buildings;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBuildingSpawnerCtrl();
        this.LoadPoolObjects();
    }

    protected virtual void LoadPoolObjects()
    {
        if (this.buildings.Count > 0) return;
        BuildingCtrl[] components = this.ctrl.Spawner.PoolHolder.GetComponentsInChildren<BuildingCtrl>();
        this.buildings = new List<BuildingCtrl>(components);
        //Debug.Log(transform.name + ": LoadPoolObjects", gameObject);
    }

    protected virtual void LoadBuildingSpawnerCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<BuildingSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadBuildingSpawnerCtrl", gameObject);
    }

    public virtual BuildingCtrl FindBuilding(BuildingType buildingType)
    {
        foreach (BuildingCtrl buildingCtrl in this.BuildingCtrls())
        {
            if (buildingCtrl.BuildingType != buildingType) continue;
            return buildingCtrl;
        }
        return null;
    }

    public virtual List<BuildingCtrl> FindBuildings(BuildingType buildingType)
    {
        return this.buildings.FindAll(building => building.BuildingType == buildingType);
    }

    public virtual BuildingHasWorkersCtrl FindWorkStation()
    {
        List<BuildingCtrl> workStations = this.FindBuildings(BuildingType.workStation);
        BuildingHasWorkersCtrl stationNeedWorker = null;
        foreach (BuildingHasWorkersCtrl buildingHasWorker in workStations)
        {
            if (!buildingHasWorker.Workers.IsNeedWorker()) continue;
            if (stationNeedWorker == null) stationNeedWorker = buildingHasWorker;

            if (stationNeedWorker.Workers.Count() > buildingHasWorker.Workers.Count())
            {
                stationNeedWorker = buildingHasWorker;
            }
        }

        return stationNeedWorker;
    }


    public virtual List<BuildingCtrl> BuildingCtrls()
    {
        return this.buildings;
    }

    public virtual List<WarehouseCtrl> Warehouses()
    {
        //TODO: cached warehouses, create protected List<WarehouseCtrl> 
        List<WarehouseCtrl> warehouses = new();
        foreach (BuildingCtrl buildingCtrl in this.buildings)
        {
            if (buildingCtrl.GetName() == BuildingName.Warehouse.ToString()) warehouses.Add((WarehouseCtrl)buildingCtrl);
        }
        return warehouses;
    }

    public virtual void Add(BuildingCtrl buildingCtrl)
    {
        this.BuildingCtrls().Add(buildingCtrl);
        this.NearBuildingRecheck();
    }

    public virtual void Remove(BuildingCtrl buildingCtrl)
    {
        this.BuildingCtrls().Remove(buildingCtrl);
    }

    protected virtual void NearBuildingRecheck()
    {
        foreach (BuildingCtrl buildingCtrl in this.BuildingCtrls())
        {
            buildingCtrl.FindNearBuildings();
        }
    }
}

