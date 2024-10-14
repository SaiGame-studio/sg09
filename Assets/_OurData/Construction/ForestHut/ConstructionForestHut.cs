using UnityEngine;

public class ConstructionForestHut : AbsConstructionIsBuilding
{
    protected override string GetBuildName()
    {
        return BuildingName.ForestHut.ToString();
    }

    protected override void LoadResRequires()
    {
        if (this.resRequires.Count > 0) return;
        this.resRequires.Add(new Resource(ResourceName.logwood,2));
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
