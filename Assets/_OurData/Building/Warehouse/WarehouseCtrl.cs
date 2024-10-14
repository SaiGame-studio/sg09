using UnityEngine;

public class WarehouseCtrl : BuildingHasWorkersCtrl
{
    public override string GetName()
    {
        return BuildingName.Warehouse.ToString();
    }
}
