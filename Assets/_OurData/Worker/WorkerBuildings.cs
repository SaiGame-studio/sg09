using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBuildings : SaiBehaviour
{
    [SerializeField] protected BuildingCtrl workBuilding;
    [SerializeField] protected BuildingCtrl homeBuilding;
    [SerializeField] protected List<BuildingCtrl> innBuildings;
    [SerializeField] protected List<BuildingCtrl> relaxBuildings;

    public virtual void AssignWork(BuildingCtrl buildingCtrl)
    {
        this.workBuilding = buildingCtrl;
    }

    public virtual BuildingCtrl GetWork()
    {
        return this.workBuilding;
    }
}