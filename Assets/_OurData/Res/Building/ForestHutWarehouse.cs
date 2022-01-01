using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestHutWarehouse : Warehouse
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.buildingType = BuildingType.workStation;
    }
}
