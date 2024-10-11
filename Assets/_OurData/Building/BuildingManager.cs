using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : SaiBehaviour
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
        BuildingCtrl buildingCtrl;
        for (int i = 0; i < this.BuildingCtrls().Count; i++)
        {
            buildingCtrl = this.BuildingCtrls()[i];
            if (!buildingCtrl.workers.IsNeedWorker()) continue;
            if (buildingCtrl.buildingType != buildingType) continue;

            return buildingCtrl;
        }
        return null;
    }

    public virtual List<BuildingCtrl> BuildingCtrls()
    {
        return this.buildings;
    }

    public virtual void AddBuilding(BuildingCtrl buildingCtrl)
    {
        this.BuildingCtrls().Add(buildingCtrl);
        buildingCtrl.transform.parent = transform;
        this.NearBuildingRecheck();
    }

    public virtual void RemoveBuilding(BuildingCtrl buildingCtrl)
    {
        this.BuildingCtrls().Remove(buildingCtrl);
    }

    protected virtual void NearBuildingRecheck()
    {
        foreach (BuildingCtrl buildingCtrl in this.BuildingCtrls())
        {
            buildingCtrl.buildingTask.FindNearBuildings();
        }
    }
}

