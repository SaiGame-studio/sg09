using UnityEngine;

public class ConstructionWarehouse : AbsConstructionIsBuilding
{
    protected override string GetBuildName()
    {
        return BuildingName.Warehouse.ToString();
    }

    protected override void LoadResRequires()
    {
        if (this.resourcesNeed.Count > 0) return;
        this.resourcesNeed.Add(new Resource(ResourceName.logwood, 1));
        this.resourcesNeed.Add(new Resource(ResourceName.blank, 1));
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
