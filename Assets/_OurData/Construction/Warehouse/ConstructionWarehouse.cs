using UnityEngine;

public class ConstructionWarehouse : AbsConstructionIsBuilding
{
    protected override string GetBuildName()
    {
        return BuildingName.Warehouse.ToString();
    }

    protected override void LoadResRequires()
    {
        if (this.resRequires.Count > 0) return;
        this.resRequires.Add(new Resource(ResourceName.logwood, 1));
        this.resRequires.Add(new Resource(ResourceName.blank, 1));
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
