using System.Collections.Generic;
using UnityEngine;

public class WorkerBuildings : SaiBehaviour
{
    [SerializeField] protected BuildingHasWorkersCtrl workBuilding;
    public BuildingHasWorkersCtrl WorkBuilding => workBuilding;
    [SerializeField] protected BuildingHasWorkersCtrl homeBuilding;
    [SerializeField] protected List<BuildingCtrl> innBuildings;
    [SerializeField] protected List<BuildingCtrl> relaxBuildings;

    public virtual void AssignWork(BuildingHasWorkersCtrl buildingCtrl)
    {
        this.workBuilding = buildingCtrl;
    }

    public virtual BuildingHasWorkersCtrl GetWork()
    {
        return this.workBuilding;
    }

    public virtual BuildingHasWorkersCtrl GetHome()
    {
        return this.homeBuilding;
    }

    public virtual void AssignHome(BuildingHasWorkersCtrl buildingCtrl)
    {
        this.homeBuilding = buildingCtrl;
    }

    public virtual void WorkerReleased()
    {
        this.workBuilding = null;
        this.homeBuilding = null;
    }
}
