using UnityEngine;

public class BuildHouseBuilder : BuildBuilding
{
    protected override void LoadResRequires()
    {
        if (this.resRequires.Count > 0) return;
        this.resRequires.Add(new Resource { name = ResourceName.logwood, number = 1 });
        Debug.Log(transform.name + ": LoadResRequires", gameObject);
    }
}
