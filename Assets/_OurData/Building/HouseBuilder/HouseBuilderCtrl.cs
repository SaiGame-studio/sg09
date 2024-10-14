using UnityEngine;

public class HouseBuilderCtrl : BuildingHasWorkersCtrl
{
    public override string GetName()
    {
        return BuildingName.HouseBuilder.ToString();
    }
}
