using UnityEngine;

public class ConstuctionWell : AbsConstructionIsBuilding
{
    protected override string GetBuildName()
    {
        return BuildingName.Well.ToString();
    }

    protected override void LoadResRequires()
    {
        if (this.resRequires.Count > 0) return;
        this.resRequires.Add(new Resource(ResourceName.logwood, 1));
        this.resRequires.Add(new Resource(ResourceName.blank, 1));
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
