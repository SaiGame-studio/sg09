using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSmallWarehouse : Warehouse
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.buildingType = BuildingType.home;
    }
}
