using UnityEngine;

public class ConstructionForestHut : AbsConstructionIsBuilding
{
    protected override string GetBuildName()
    {
        return BuildingName.ForestHut.ToString();
    }

    protected override void LoadResRequires()
    {
        if (this.resourcesNeed.Count > 0) return;
        this.resourcesNeed.Add(new Resource(ResourceName.logwood,2));
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
