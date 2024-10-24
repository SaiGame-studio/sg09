using UnityEngine;

public class ConstructionSawmill : AbsConstructionIsBuilding
{
    protected override string GetBuildName()
    {
        return BuildingName.Sawmill.ToString();
    }

    protected override void LoadResRequires()
    {
        if (this.resourcesNeed.Count > 0) return;
        this.resourcesNeed.Add(new Resource(ResourceName.logwood, 5));
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
