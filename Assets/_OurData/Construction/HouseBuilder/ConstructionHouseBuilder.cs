using UnityEngine;

public class ConstructionHouseBuilder : AbsConstructionIsBuilding
{
    protected override string GetBuildName()
    {
        return BuildingName.HouseBuilder.ToString();
    }

    protected override void LoadResRequires()
    {
        if (this.resourcesNeed.Count > 0) return;
        this.resourcesNeed.Add(new Resource(ResourceName.logwood, 1));
        this.resourcesNeed.Add(new Resource(ResourceName.blank, 1));
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
