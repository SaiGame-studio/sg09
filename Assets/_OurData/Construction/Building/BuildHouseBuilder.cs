using UnityEngine;

public class BuildHouseBuilder : BuildBuilding
{

    protected override Transform CreateBuild()
    {
        throw new System.NotImplementedException();
    }

    protected override string GetBuildName()
    {
        throw new System.NotImplementedException();
    }
    protected override void LoadResRequires()
    {
        if (this.resRequires.Count > 0) return;
        this.resRequires.Add(new Resource { name = ResourceName.logwood, number = 1 });
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
