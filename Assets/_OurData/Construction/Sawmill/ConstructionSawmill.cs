using UnityEngine;

public class ConstructionSawmill : AbsConstructionIsBuilding
{
    protected override string GetBuildName()
    {
        return BuildingName.Sawmill.ToString();
    }

    protected override void LoadResRequires()
    {
        if (this.resRequires.Count > 0) return;
        this.resRequires.Add(new Resource { name = ResourceName.logwood, number = 5 });
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
